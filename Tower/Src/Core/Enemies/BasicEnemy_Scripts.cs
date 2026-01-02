using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Helpers;
using Tower.Managers;

namespace Tower.Core.Enemies;

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
            // Сохраняем время атаки и позицию цели для анимации
            _lastAttackTime = gameTime.TotalGameTime;
            _lastAttackTargetPosition = citadel.Position;

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
        
        // Вычисляем анимацию атаки (пульсация и изменение цвета)
        var attackAnimationProgress = 0.0f;
        var isAttacking = false;
        Vector2? attackTargetPos = null;
        if (_lastAttackTime != TimeSpan.Zero)
        {
            var timeSinceAttack = gameTime.TotalGameTime - _lastAttackTime;
            if (timeSinceAttack < AttackAnimationDuration)
            {
                isAttacking = true;
                // Прогресс анимации от 0 до 1
                attackAnimationProgress = (float)(timeSinceAttack.TotalMilliseconds / AttackAnimationDuration.TotalMilliseconds);
                attackTargetPos = _lastAttackTargetPosition;
            }
            else
            {
                _lastAttackTime = TimeSpan.Zero; // Сбрасываем после завершения анимации
                _lastAttackTargetPosition = null;
            }
        }
        
        // Вычисляем эффекты анимации атаки
        var attackScale = 1.0f;
        var attackColor = Color.White;
        if (isAttacking)
        {
            // Пульсация: масштаб от 1.0 до 1.3 и обратно (синусоида)
            var pulse = (float)Math.Sin(attackAnimationProgress * Math.PI);
            attackScale = 1.0f + pulse * 0.3f;
            
            // Изменение цвета: от белого к красному и обратно
            var redIntensity = (float)Math.Sin(attackAnimationProgress * Math.PI);
            attackColor = new Color(
                (byte)255,
                (byte)(255 - redIntensity * 100), // Уменьшаем зеленый
                (byte)(255 - redIntensity * 100)  // Уменьшаем синий
            );
        }
        
        // Отрисовка спрайта врага с анимацией атаки
        var finalScale = SpriteScale * attackScale;
        spriteBatch.Draw(
            Sprite,
            Position,
            null,
            attackColor,
            0f,
            new Vector2((float)Sprite.Width / 2, (float)Sprite.Height / 2),
            finalScale,
            SpriteEffects.None,
            0.5f
        );
        
        // Отрисовка визуального эффекта атаки (луч от врага к цели)
        if (isAttacking && attackTargetPos.HasValue)
        {
            DrawAttackEffect(spriteBatch, attackTargetPos.Value, attackAnimationProgress, gameTime);
        }
        
        // Отрисовка полосы здоровья
        HealthBar.Draw(
            spriteBatch,
            gameTime,
            this
        );
        spriteBatch.End();
    }
    
    /// <summary>
    /// Отрисовка визуального эффекта атаки - луч от врага к цели.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="targetPosition">Позиция цели атаки</param>
    /// <param name="animationProgress">Прогресс анимации (0.0 - 1.0)</param>
    /// <param name="gameTime">Время игры для создания текстуры</param>
    private void DrawAttackEffect(SpriteBatch spriteBatch, Vector2 targetPosition, float animationProgress, GameTime gameTime)
    {
        // Получаем или создаем текстуру для линии атаки
        var lineTexture = GetAttackLineTexture();
        if (lineTexture == null) return;
        
        // Вычисляем направление от врага к цели
        var direction = targetPosition - Position;
        var distance = direction.Length();
        
        if (distance <= 0) return;
        
        direction = Vector2.Normalize(direction);
        
        // Вычисляем угол поворота линии
        var angle = (float)Math.Atan2(direction.Y, direction.X);
        
        // Вычисляем длину линии с учетом анимации (линия "выстреливает" от врага к цели)
        var lineLength = distance * animationProgress;
        
        // Вычисляем цвет линии (ярко-красный, становится прозрачнее к концу анимации)
        var alpha = (byte)(255 * (1.0f - animationProgress * 0.5f)); // От 255 до 127
        var lineColor = new Color((byte)255, (byte)50, (byte)50, alpha);
        
        // Отрисовываем линию атаки
        spriteBatch.Draw(
            lineTexture,
            Position, // Начальная позиция (позиция врага)
            null,
            lineColor,
            angle, // Угол поворота
            Vector2.Zero, // Точка привязки
            new Vector2(lineLength, 3.0f), // Масштаб (длина и толщина линии)
            SpriteEffects.None,
            0.6f // Глубина слоя (между врагами и снарядами)
        );
    }
    
    /// <summary>
    /// Получить или создать текстуру для линии атаки.
    /// Использует HealthBarSprite как однопиксельную текстуру.
    /// </summary>
    /// <returns>Текстура для линии атаки</returns>
    private Texture2D GetAttackLineTexture()
    {
        // Используем существующую однопиксельную текстуру из SpriteManager
        return SpriteManager.HealthBarSprite;
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