using Structurizr;

namespace Droplet_c4_model_design {
    public class IdentityAccessComponentDiagram {
        // C4 Model
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "IdentityAccessComponent";
        
        // Components
        public Component AuthController { get; private set; }
        public Component AuthService { get; private set; }
        public Component UserRepository { get; private set; }
        public Component UserEntity { get; private set; }
        
        // Constructor
        public IdentityAccessComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram) {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
            this.containerDiagram = containerDiagram;
        }

        // Generate Method
        public void Generate() {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Components
        private void AddComponents() {
            AuthController = containerDiagram.RestApi.AddComponent(
                "Auth Controller",
                "Maneja solicitudes de autenticación (login, logout, registro) y emisión de tokens JWT.",
                "ASP.NET Core Controller"
            );
            
            AuthService = containerDiagram.RestApi.AddComponent(
                "Auth Service",
                "Contiene la lógica de negocio para autenticación, generación y validación de tokens JWT.",
                "C# Service"
            );

            UserRepository = containerDiagram.RestApi.AddComponent(
                "User Repository",
                "Gestiona la persistencia y recuperación de datos de usuarios desde la base de datos.",
                "C# Repository"
            );
                
            UserEntity = containerDiagram.RestApi.AddComponent(
                "User Entity",
                "Entidad de dominio que representa a un usuario con propiedades como Id, Username, PasswordHash, Roles, Email, Preferencias.",
                "C# Class"
            );
        }
        
        // Add Relationships
        private void AddRelationships() {
            // People to Components
            contextDiagram.Jefe_de_Hogar.Uses(
                AuthController,
                "Se registra, inicia sesión y obtiene token JWT"
            );

            contextDiagram.Maestro_Cervecero.Uses(
                AuthController,
                "Se registra, inicia sesión y obtiene token JWT"
            );

            contextDiagram.Fluix_Administrator.Uses(
                AuthController,
                "description"
                );

            // Components to Components
            AuthController.Uses(
                AuthService,
                "Invoca para validar credenciales y generar tokens"
            );

            AuthService.Uses(
                UserRepository,
                "Consulta y actualiza información de usuarios"
            );

            UserRepository.Uses(
                UserEntity,
                "Mapea datos entre DB y modelo de dominio"
            );

            // Components to Containers
            UserRepository.Uses(
                containerDiagram.Database,
                "Lee y escribe información de usuarios",
                "SQL"
            );
        }

        // Apply Styles
        private void ApplyStyles() {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;
            
            // Components
            styles.Add(new ElementStyle(componentTag) {
                Background = "#6a1b9a", 
                Color = "#ffffff", 
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags() {
            AuthController.AddTags(componentTag);
            AuthService.AddTags(componentTag);
            UserRepository.AddTags(componentTag);
            UserEntity.AddTags(componentTag);
        }

        // Create View
        private void CreateView() {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi, 
                "droplet-component-identity-access",
                "Component Diagram - Identity & Access Bounded Context"
            );
            
            // Title
            componentView.Title = "Droplet - Identity & Access";
            
            // Elements
            componentView.Add(AuthController);
            componentView.Add(AuthService);
            componentView.Add(UserRepository);
            componentView.Add(UserEntity);

            // People
            componentView.Add(contextDiagram.Jefe_de_Hogar);
            componentView.Add(contextDiagram.Maestro_Cervecero);
            componentView.Add(contextDiagram.Fluix_Administrator);
            
            // Database
            componentView.Add(containerDiagram.Database);
        }
    }
}
