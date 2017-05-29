using Holoville.HOTween;
using Svelto.IoC;
using Svelto.Context;
using Svelto.Factories;

//Main is the Application Composition Root.
//Composition Root is the place where the framework can be initialised.

public class Main:ICompositionRoot
{
    public IContainer container { get; private set; }
    
    public Main()
    {
        SetupContainer();
    }
    
    void SetupContainer()
    {
        container = new Container();
        
        //interface is bound to a specific instance
        container.Bind<IGameObjectFactory>().AsInstance(new Svelto.IoC.GameObjectFactory(container));
        //interfaces are bound to specific implementations, the same implementation will be known 
        //through two different interfaces.
        container.Bind<IMonsterCounter>().AsSingle<MonsterCountHolder>();
        container.Bind<IMonsterCountHolder>().AsSingle<MonsterCountHolder>();
        //exploit the providers to add new functionalities. In this case a new instance
        //will be created for each requested injection.
        container.Bind<WeaponPresenter>().ToProvider(new MultiProvider<WeaponPresenter>());
        container.Bind<MonsterPresenter>().ToProvider(new MultiProvider<MonsterPresenter>());
        container.Bind<MonsterPathFollower>().ToProvider(new MultiProvider<MonsterPathFollower>());
        //once requested, the same instance will be used. Use this form of 
        //dependency binding sparingly and only if you have Inversion of Control
        //in mind, otherwise it will be used as a singleton substitute, which is
        //a design mistake. Theoretically these objects should always require
        //template pattern objects to be registered in.
        container.BindSelf<UnderAttackSystem>();
        container.BindSelf<PathController>();
        //the composition root and the factories are the only places
        //where a container can be used diretly. Inject a container
        //otherwise would be an error! 
        //_monsterSpawner will never be injected anywhere, but
        //it needs dependencies injected.
        _monsterSpawner = container.Inject(new MonsterSpawner());
    }
    
    public void OnContextInitialized()
    {
        HOTween.Init(false, false, true);
        HOTween.EnableOverwriteManager();
    }

    public void OnContextDestroyed()
    {
    }

    public void OnContextCreated(UnityContext contextHolder)
    {
        //inject all the gameobjects in the scene child of the GameContext
        foreach (UnityEngine.Transform transform in contextHolder.transform)
        {
            var monobehaviours = transform.GetComponentsInChildren<UnityEngine.MonoBehaviour>();

            for (int i = 0; i < monobehaviours.Length; i++)
                container.Inject(monobehaviours[i]);
        }
    }

    MonsterSpawner _monsterSpawner;
}

//A GameObject containing GameContext must be present in the scene
//All the monobehaviours present in the scene file that need dependencies 
//injected must be component of GameObjects children of GameContext.

public class GameContext: UnityContext<Main>
{
}
