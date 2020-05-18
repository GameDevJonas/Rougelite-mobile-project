using System;
public class HealthSystem //new class
{
    private float health; //health number
    private float healthMax;
    public HealthSystem(float healthMax) //class handles health number
    {
        this.healthMax = healthMax; //setting health in other scripts defines the current health value
        health = healthMax;
    }
    public void CurrentHealth(float healthCurrent)
    {
        this.health = healthCurrent;
        health = healthCurrent;
    }
    public float GetHealth() //request current hp
    {
        return health; //get current hp
    }

    public void ModMaxHealth(float MaxHealth)
    {
        this.healthMax = MaxHealth;
        healthMax = MaxHealth;
    }

    public void Damage(float damageAmount) //damage is decided by damage amount
    {
        health -= damageAmount; //subtract damage amount from health
        if (health < 0) health = 0;//health cannot go below zero
    }
    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
    }
}
