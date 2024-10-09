using ReactiveUI;
using SignalViewer.Views;
using Splat;

namespace SignalViewer.ViewModels;

public class AppBootstrapper: ReactiveObject, IScreen
{
    public AppBootstrapper(IReadonlyDependencyResolver resolver = null, IMutableDependencyResolver dependencyResolver  = null, RoutingState testRouter = null)
    {
        Router = testRouter ?? new RoutingState();
        resolver = resolver ?? Locator.Current;
        dependencyResolver = dependencyResolver ?? Locator.CurrentMutable;

        RegisterParts(dependencyResolver);

        // Navigate to the opening page of the application
        Router.Navigate.Execute(resolver.GetService<IMainViewModel>()).Subscribe();
        
    }
    public RoutingState Router { get; }
    
    private void RegisterParts(IMutableDependencyResolver dependencyResolver)
    {
        // Make sure Splat and ReactiveUI are already configured in the locator
        // so that our override runs last
        Locator.CurrentMutable.InitializeSplat();
        Locator.CurrentMutable.InitializeReactiveUI();

        dependencyResolver.RegisterConstant<IScreen>(this);
        // dependencyResolver.RegisterConstant<IRepositoryViewModelFactory>(new DefaultRepositoryViewModelFactory());
        // dependencyResolver.RegisterConstant<IRepositoryFactory>(new DefaultRepositoryFactory());
        // dependencyResolver.RegisterConstant<IWindowLayoutViewModel>(new WindowLayoutViewModel());
        // dependencyResolver.Register<ILayoutViewModel>(() => new LayoutViewModel());

        var locator = Locator.Current;

        dependencyResolver.Register<IMainViewModel>(() => new MainViewModel(locator.GetService<IScreen>()));
        dependencyResolver.Register<IViewFor<MainViewModel>>(() => new MainView());
        dependencyResolver.Register<IViewFor<ProjectViewModel>>(() => new ProjectView());
        dependencyResolver.Register<IViewFor<MeasurementViewModel>>(() => new MeasurementView());
        dependencyResolver.Register<IViewFor<DataChartViewModel>>(() => new DataChartView());
        dependencyResolver.Register<IViewFor<DataTableViewModel>>(() => new DataTableView());
    }
}