internal class MonsterCountHolder : IMonsterCounter, IMonsterCountHolder
{
    //IMonsterCounter Interface
    public int monsterCount { get { return _count; } }

    //IMonsterCountHolder interface
    public void AddMonster() { _count++; }
    public void RemoveMonster() { _count--; }

    int   _count;
}
