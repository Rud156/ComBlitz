using UnityEngine;

namespace ComBlitz.Player.Data
{
    public static class PlayerConstantData
    {
        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";

        public const string PlayerHorizontalMovement = "PlayerHorizontalMovement";
        public const string PlayerVerticalMovement = "PlayerVerticalMovement";
        public const string PlayerShootAnimParam = "PlayerShooting";

        public static readonly LayerMask spawnerMask = 1 << 9 | 1 << 10 | 1 << 13 | 1 << 14;
    }
}