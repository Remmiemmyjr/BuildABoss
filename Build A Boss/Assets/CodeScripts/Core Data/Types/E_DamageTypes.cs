using UnityEngine;


[System.Flags] // Flags + Powers of 2 allow multi selection in dropdown
public enum DamageType
{
    None        = 0,
    Bludgeoning = 2,
    Piercing    = 4,
    Slashing    = 8,
    Projectile  = 16,
    Blast       = 32,
    Poison      = 64,
    Fire        = 128,
    Ice         = 256,
    Lightning   = 512,
    Necrotic    = 1024,
    Radiant     = 2048,
    Psychic     = 4096,
    Emotional   = 8192
}
