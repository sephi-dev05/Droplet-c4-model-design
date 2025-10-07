using Structurizr;

namespace Droplet_c4_model_design
{
    public class DashboardAnalyticsComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "DashboardAnalyticsComponent";

        // Components
        public Component DashboardController { get; private set; }
        public Component ReportController { get; private set; }
        public Component DashboardService { get; private set; }
        public Component ReportService { get; private set; }
        public Component ReportRepository { get; private set; }
        public Component ReportEntity { get; private set; }
        public Component DashboardReadModel { get; private set; }

        // Constructor
        public DashboardAnalyticsComponentDiagram(C4 c4, ContextDiagram contextDiagram,
            ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
            this.containerDiagram = containerDiagram;
        }

        // Generate Method
        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Components
        private void AddComponents()
        {
            DashboardController = containerDiagram.RestApi.AddComponent(
                "Dashboard Controller",
                "",
                ""
            );

            ReportController = containerDiagram.RestApi.AddComponent(
                "Report Controller",
                "",
                ""
            );

            DashboardService = containerDiagram.RestApi.AddComponent(
                "Dashboard Service",
                "",
                ""
            );

            ReportService = containerDiagram.RestApi.AddComponent(
                "Report Service",
                "",
                ""
            );

            ReportRepository = containerDiagram.RestApi.AddComponent(
                "Report Repository",
                "",
                ""
            );

            ReportEntity = containerDiagram.RestApi.AddComponent(
                "Report Entity",
                "",
                ""
            );

            DashboardReadModel = containerDiagram.RestApi.AddComponent(
                "Dashboard Read Model",
                "",
                ""
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            //People to Components
            contextDiagram.Jefe_de_Hogar.Uses(
                DashboardController,
                ""
            );
           
            contextDiagram.Jefe_de_Hogar.Uses(
                ReportController,
                ""
            );

            contextDiagram.Maestro_Cervecero.Uses(
                DashboardController,
                ""
            );
            
            contextDiagram.Maestro_Cervecero.Uses(
                ReportController,
                ""
            );

            contextDiagram.Fluix_Administrator.Uses(
                DashboardController,
                ""
            );

            contextDiagram.Fluix_Administrator.Uses(
                ReportController,
                ""
            );

            // Components to Components
            DashboardController.Uses(
                DashboardService,
                ""
            );

            DashboardService.Uses(
                DashboardReadModel,
                ""
            );

            DashboardReadModel.Uses(
                containerDiagram.Database,
                "",
                "SQL"
            );

            ReportController.Uses(
                ReportService,
                ""
            );

            ReportService.Uses(
                ReportRepository,
                ""
                );

            ReportRepository.Uses(
                ReportEntity,
                ""
            );

            ReportRepository.Uses(
                containerDiagram.Database,
                "",
                "SQL"
            );
        }

        // Apply Styles
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            // Components
            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#1565c0", // strong blue (Dashboard & Analytics)
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags()
        {
            // Components
            DashboardController.AddTags(componentTag);
            ReportController.AddTags(componentTag);
            DashboardService.AddTags(componentTag);
            ReportService.AddTags(componentTag);
            ReportRepository.AddTags(componentTag);
            ReportEntity.AddTags(componentTag);
            DashboardReadModel.AddTags(componentTag);
        }

        // Create View
        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi,
                "droplet-component-dashboard-analytics",
                "Component Diagram - Dashboard & Analytics Bounded Context"
            );

            // Title
            string title = "Droplet - Dashboard & Analytics";
            componentView.Title = title;

            // Elements to add to the view
            componentView.Add(DashboardController);
            componentView.Add(ReportController);
            componentView.Add(DashboardService);
            componentView.Add(ReportService);
            componentView.Add(ReportRepository);
            componentView.Add(ReportEntity);
            componentView.Add(DashboardReadModel);

            // People to add to the view
            componentView.Add(contextDiagram.Maestro_Cervecero);
            componentView.Add(contextDiagram.Jefe_de_Hogar);
            componentView.Add(contextDiagram.Fluix_Administrator);
            componentView.Add(containerDiagram.Database);
        }
    }
}