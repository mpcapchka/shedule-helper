using Microsoft.Extensions.DependencyInjection;
using SheduleHelper.WpfApp.Services;
using SheduleHelper.WpfApp.ViewModel;
using System.IO.Abstractions;
using System.Windows;
using System.Windows.Media;

namespace SheduleHelper.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        #region Constructors
        public App()
        {
            Services = ConfigureServices();
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.Default;
            this.InitializeComponent();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }
        #endregion

        #region Methods

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton<LoggingService>();

            services.AddSingleton<SettingsTabViewModel>();
            services.AddSingleton<SheduleTabViewModel>();
            services.AddSingleton<DashboardTabViewModel>();
            services.AddSingleton<TasksTabViewModel>();

            return services.BuildServiceProvider();
        }
        #endregion

        #region Handlers
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Resolve and initialize logging service
            var loggingService = Services.GetService<LoggingService>();
            loggingService?.Initialize();
        }
        #endregion
    }

}
