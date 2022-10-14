class CmdDamage : ICommand
{
    private IDamageable _damageable;
    private IDamager _damager;

    public CmdDamage(IDamager damager, IDamageable damageable){
        _damager = damager;
        _damageable = damageable;
    }
    public void Execute() => _damager.DealDamage(_damageable);
}
