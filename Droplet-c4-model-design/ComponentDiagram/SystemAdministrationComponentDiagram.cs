using Structurizr;

namespace Droplet_c4_model_design
{
    public class SystemAdministrationComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "SystemAdministrationComponent";

        // Components
        public Component AdminController { get; private set; }
        public Component UserManagementService { get; private set; }
        public Component ConfigService { get; private set; }
        public Component AuditLogService { get; private set; }
        public Component AdminRepository { get; private set; }
        public Component AdminEntity { get; private set; }

        // Constructor
        public SystemAdministrationComponentDiagram(C4 c4, ContextDiagram contextDiagram,
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
            AdminController = containerDiagram.RestApi.AddComponent(
                "Admin Controller",
                "Handles administration requests: users, configurations, and incidents.",
                "ASP.NET Core Controller"
            );

            UserManagementService = containerDiagram.RestApi.AddComponent(
                "User Management Service",
                "Manages internal users, roles, and permissions.",
                "C# Service"
            );

            ConfigService = containerDiagram.RestApi.AddComponent(
                "Config Service",
                "Maintains system configuration parameters.",
                "C# Service"
            );

            AuditLogService = containerDiagram.RestApi.AddComponent(
                "Audit Log Service",
                "Manages log records and incidents for auditing.",
                "C# Service"
            );

            AdminRepository = containerDiagram.RestApi.AddComponent(
                "Admin Repository",
                "Manages persistence of internal users, configurations, and logs in the database.",
                "C# Repository"
            );

            AdminEntity = containerDiagram.RestApi.AddComponent(
                "Admin Entity",
                "Domain entity with users, roles, configurations, and logs.",
                "C# Class"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // People to Components
            contextDiagram.Fluix_Administrator.Uses(
                AdminController,
                "Manages configurations, internal users, and incidents"
            );

            // Components to Components
            AdminController.Uses(
                UserManagementService,
                "Delegates user and role logic"
            );

            AdminController.Uses(
                ConfigService,
                "Delegates configuration logic"
            );

            AdminController.Uses(
                AuditLogService,
                "Delegates log management"
            );

            UserManagementService.Uses(
                AdminRepository,
                "Stores and retrieves internal users"
            );

            ConfigService.Uses(
                AdminRepository,
                "Stores and retrieves configurations"
            );

            AuditLogService.Uses(
                AdminRepository,
                "Records logs and incidents"
            );

            AdminRepository.Uses(
                AdminEntity,
                "Maps database data to the domain model"
            );

            AdminRepository.Uses(
                containerDiagram.Database,
                "Reads and writes internal users, configurations, and logs",
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
                Background = "#455a64", // gray (System Administration)
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags()
        {
            // Components
            AdminController.AddTags(componentTag);
            UserManagementService.AddTags(componentTag);
            ConfigService.AddTags(componentTag);
            AuditLogService.AddTags(componentTag);
            AdminRepository.AddTags(componentTag);
            AdminEntity.AddTags(componentTag);
        }

        // Create View
        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi,
                "glassgo-component-system-administration",
                "Component Diagram - System Administration Bounded Context"
            );

            // Title
            string title = "GlassGo - System Administration";
            componentView.Title = title;

            // Elements to add to the view
            componentView.Add(AdminController);
            componentView.Add(UserManagementService);
            componentView.Add(ConfigService);
            componentView.Add(AuditLogService);
            componentView.Add(AdminRepository);
            componentView.Add(AdminEntity);

            // People to add to the view
            componentView.Add(contextDiagram.Fluix_Administrator);
            componentView.Add(containerDiagram.Database);
        }
    }
}