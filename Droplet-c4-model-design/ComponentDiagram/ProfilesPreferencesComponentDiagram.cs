using Structurizr;

namespace Droplet_c4_model_design
{
    public class ProfilesPreferencesComponentDiagram
    {
        // C4 Model
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "ProfilesPreferencesComponent";

        // Components
        public Component ProfileController { get; private set; }
        public Component PreferencesController { get; private set; }
        public Component ProfileService { get; private set; }
        public Component PreferencesService { get; private set; }
        public Component ProfileRepository { get; private set; }
        public Component ProfileEntity { get; private set; }

        // Constructor
        public ProfilesPreferencesComponentDiagram(C4 c4, ContextDiagram contextDiagram,
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
            ProfileController = containerDiagram.RestApi.AddComponent(
                "Profile Controller",
                "Maneja solicitudes de visualización y edición de perfiles de usuario (hogar / pyme).",
                "ASP.NET Core Controller"
            );

            PreferencesController = containerDiagram.RestApi.AddComponent(
                "Preferences Controller",
                "Maneja solicitudes de configuración de idioma, alertas y canales de notificación.",
                "ASP.NET Core Controller"
            );

            ProfileService = containerDiagram.RestApi.AddComponent(
                "Profile Service",
                "Lógica de negocio para validar y aplicar cambios en perfiles de usuario.",
                "C# Service"
            );

            PreferencesService = containerDiagram.RestApi.AddComponent(
                "Preferences Service",
                "Lógica de negocio para aplicar preferencias de notificación y configuración de alertas.",
                "C# Service"
            );

            ProfileRepository = containerDiagram.RestApi.AddComponent(
                "Profile Repository",
                "Persistencia de perfiles y preferencias de usuario en la base de datos.",
                "C# Repository"
            );

            ProfileEntity = containerDiagram.RestApi.AddComponent(
                "Profile Entity",
                "Entidad de dominio con información de perfil (nombre, email, tipo usuario) y preferencias (idioma, notificaciones, alertas).",
                "C# Class"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // People to Components
            contextDiagram.Jefe_de_Hogar.Uses(
                ProfileController,
                "Actualiza su perfil y preferencias en la Web/Mobile App"
            );

            contextDiagram.Jefe_de_Hogar.Uses(
                PreferencesController,
                "Configura notificaciones y alertas"
            );

            contextDiagram.Maestro_Cervecero.Uses(
                ProfileController,
                "Gestiona perfil de la empresa cervecera"
            );

            contextDiagram.Maestro_Cervecero.Uses(
                PreferencesController,
                "Configura alertas críticas y preferencias de idioma"
            );

            contextDiagram.Fluix_Administrator.Uses(
                ProfileController,
                "description"
            );

 

            // Components to Components
            ProfileController.Uses(ProfileService, "Delegación de lógica de negocio de perfiles");
            PreferencesController.Uses(PreferencesService, "Delegación de lógica de negocio de preferencias");
            ProfileService.Uses(ProfileRepository, "Gestiona datos de perfiles");
            PreferencesService.Uses(ProfileRepository, "Gestiona datos de preferencias");
            ProfileRepository.Uses(ProfileEntity, "Mapea datos entre DB y modelo de dominio");

            // Components to Containers
            ProfileRepository.Uses(containerDiagram.Database, "Lee y escribe perfiles y preferencias", "SQL");
        }

        // Apply Styles 
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            // Components
            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#2e7d32", // verde para Profiles & Preferences
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags()
        {
            ProfileController.AddTags(componentTag);
            PreferencesController.AddTags(componentTag);
            ProfileService.AddTags(componentTag);
            PreferencesService.AddTags(componentTag);
            ProfileRepository.AddTags(componentTag);
            ProfileEntity.AddTags(componentTag);
        }

        // Create View
        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi,
                "droplet-component-profiles-preferences",
                "Component Diagram - Profiles & Preferences Bounded Context"
            );

            // Title
            componentView.Title = "Droplet - Profiles & Preferences";

            // Elements
            componentView.Add(ProfileController);
            componentView.Add(PreferencesController);
            componentView.Add(ProfileService);
            componentView.Add(PreferencesService);
            componentView.Add(ProfileRepository);
            componentView.Add(ProfileEntity);

            // People
            componentView.Add(contextDiagram.Jefe_de_Hogar);
            componentView.Add(contextDiagram.Maestro_Cervecero);
            componentView.Add(contextDiagram.Fluix_Administrator);

            // Database
            componentView.Add(containerDiagram.Database);
        }
    }
}