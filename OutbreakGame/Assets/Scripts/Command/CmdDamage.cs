class CmdDamage : ICommand
{
    private float _damage;
    private IDamageable _damageable;

    public CmdDamage(IDamageable damageable ,float damage){
        _damage = damage;
        _damageable = damageable;
    }
    public void Execute()=> _damageable.TakeDamage(_damage);
}
