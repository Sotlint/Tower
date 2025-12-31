using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Helpers;
using Tower.Managers;

namespace Tower.Core;

public partial class BasicEnemy : IEnemy
{
    /// <summary>
    /// Получение урона врагом. Уменьшает здоровье на указанное количество.
    /// </summary>
    /// <param name="damage">Количество урона</param>
    public void TakeDamage(int damage)
        => Health -= damage;

    /// <summary>
    /// Проверка, уничтожен ли враг (здоровье <= 0).
    /// </summary>
    /// <returns>true, если враг уничтожен, false - иначе</returns>
    public bool IsDie() => Health <= 0;

    /// <summary>
    /// Движение врага к указанной позиции. Вычисляет направление и перемещает врага.
    /// </summary>
    /// <param name="targetPosition">Целевая позиция (обычно позиция цитадели)</param>
    public void Move(Vector2 targetPosition)
    {
        // Вычисляем направление к цели
        var direction = targetPosition - Position;

        // Проверка, чтобы не делить на 0 (если цель уже достигнута)
        if (direction.Length() > 0)
        {
            // Нормализуем направление и перемещаем врага
            direction = Vector2.Normalize(direction);
            SetPosition(direction * Speed);
        }
    }

    /// <summary>
    /// Обновление врага. Вызывается каждый кадр во время активной волны.
    /// Обновляет границы, разрешает коллизии, проверяет расстояние до цитадели
    /// и либо атакует, либо движется к цитадели.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public void Update(GameTime gameTime)
    {
        // Обновляем границы коллизий
        UpdateBounds();
        // Разрешаем коллизии с другими объектами
        ResolveCollision();

        // Если враг мертв - прекращаем обновление
        if (IsDie())
            return;

        // Получаем цитадель
        var citadel = GameManager.CitadelManager.GetCitadel();
        
        // Проверяем, в радиусе ли атаки цитадель
        if (Vector2.Distance(Position, citadel.Position) <= AttackRange)
        {
            // Увеличиваем время с последней атаки
            TimeSinceLastAttack += gameTime.ElapsedGameTime;
            
            // Проверяем, прошла ли задержка между атаками
            if (TimeSinceLastAttack < AttackDelay)
                return;

            // Атакуем цитадель
            Attack(new List<ICanDie>() { citadel });
            TimeSinceLastAttack = TimeSpan.Zero;

            return;
        }

        // Если цитадель не в радиусе атаки - движемся к ней
        Move(citadel.Position);

        // Увеличиваем время с последней атаки
        TimeSinceLastAttack += gameTime.ElapsedGameTime;
    }

    /// <summary>
    /// Отрисовка врага. Отрисовывает спрайт врага, полосу здоровья
    /// и отладочную информацию в режиме отладки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin();
        
        // Отрисовка отладочной информации (границы и радиус атаки) в режиме отладки
        if (GameManager.InputManager.IsDebugMode)
        {
            DebugBorderDrawer.DrawDebug(spriteBatch, Bounds, AttackRange, Position);
        }
        
        // Отрисовка спрайта врага
        spriteBatch.DrawSprite(Sprite, Position, SpriteScale);
        
        // Отрисовка полосы здоровья
        HealthBar.Draw(
            spriteBatch,
            gameTime,
            this
        );
        spriteBatch.End();
    }

    /// <summary>
    /// Атака целей. Наносит урон указанным целям (обычно цитадели).
    /// </summary>
    /// <param name="targets">Список целей для атаки</param>
    public void Attack(IEnumerable<ICanDie> targets)
    {
        // Наносим урон каждой цели
        foreach (var target in targets)
        {
            target.TakeDamage(AttackPower);
        }
    }

    /// <summary>
    /// Разрешение коллизий врага. Обрабатывает коллизии с другими врагами и зданиями.
    /// </summary>
    public void ResolveCollision()
    {
        // Разрешаем коллизии с другими врагами
        ResolveEnemyCollision();
        // Разрешаем коллизии со зданиями (башнями и цитаделью)
        ResolveBuildingsCollision();
    }

    /// <summary>
    /// Разрешение коллизий со зданиями (башнями и цитаделью).
    /// Сдвигает врага, если он пересекается со зданием.
    /// </summary>
    private void ResolveBuildingsCollision()
    {
        // Получаем все башни и цитадель
        var towers = GameManager.TowerManager.GetTowers().Select(x => (IBuilding)x).ToList();
        var citadel = (IBuilding)GameManager.CitadelManager.GetCitadel();

        // Проверяем коллизии с каждым зданием
        foreach (var building in towers.Concat(new List<IBuilding>() { citadel }))
        {
            if (Bounds.Intersects(building.Bounds))
            {
                // Вычисляем направление для отталкивания от здания
                var moveDirection = Position - building.Position;
                
                // Если враг находится точно в центре здания - создаем случайное направление
                if (moveDirection == Vector2.Zero)
                {
                    moveDirection = new Vector2(
                        (float)(0.5 - Random.Shared.NextDouble()),
                        (float)(0.5 - Random.Shared.NextDouble())
                    );
                }

                // Нормализуем направление
                moveDirection = Vector2.Normalize(moveDirection);
                
                // Вычисляем глубину пересечения
                var overlap = (Bounds.Width / 2f + building.Bounds.Width / 2f) -
                              Vector2.Distance(Position, building.Position);
                
                // Сдвигаем врага от здания
                Position += moveDirection * (overlap / 2);
                SetPosition(moveDirection * (overlap / 2));
            }
        }
    }

    /// <summary>
    /// Разрешение коллизий с другими врагами.
    /// Сдвигает врагов, если они пересекаются друг с другом.
    /// </summary>
    private void ResolveEnemyCollision()
    {
        // Получаем всех других врагов (исключая текущего)
        var enemies = GameManager.EnemyManager.GetEnemies().Where(x => x.Id != Id).ToList();
        
        // Проверяем коллизии с каждым врагом
        foreach (var enemy in enemies)
        {
            var enemyBounds = enemy.Bounds;
            
            // Проверяем пересечение
            if (Bounds.Intersects(enemyBounds))
            {
                // Вычисляем направление, чтобы раздвинуть врагов
                var moveDirection = Position - enemy.Position;
                
                // Если враги находятся на одном месте, создаем случайное направление
                if (moveDirection == Vector2.Zero)
                {
                    moveDirection = new Vector2(
                        (float)(0.5 - Random.Shared.NextDouble()),
                        (float)(0.5 - Random.Shared.NextDouble())
                    );
                }

                // Нормализуем направление
                moveDirection = Vector2.Normalize(moveDirection);
                
                // Вычисляем глубину пересечения
                var overlap = (Bounds.Width / 2f + enemyBounds.Width / 2f) -
                              Vector2.Distance(Position, enemy.Position);
                
                // Сдвигаем врагов в разные стороны
                Position += moveDirection * (overlap / 2);
                SetPosition(moveDirection * (overlap / 2));
            }
        }
    }

    /// <summary>
    /// Обновление границ врага. Вычисляет прямоугольник коллизий на основе
    /// позиции и размера спрайта с учетом масштаба.
    /// </summary>
    public void UpdateBounds()
    {
        // Вычисляем размеры с учетом масштаба
        var width = (int)(Sprite.Width * SpriteScale);
        var height = (int)(Sprite.Height * SpriteScale);

        // Создаем прямоугольник с центрированием по позиции
        Bounds = new Rectangle(
            (int)(Position.X - width / 2), // Центрирование по X
            (int)(Position.Y - height / 2), // Центрирование по Y
            width,
            height
        );
    }

    /// <summary>
    /// Установка позиции врага. Добавляет смещение к текущей позиции.
    /// </summary>
    /// <param name="direction">Направление и расстояние для перемещения</param>
    public void SetPosition(Vector2 direction)
        => Position += direction;
}