using Structurizr;

namespace Droplet_c4_model_design {
    public class SupportNotificationsComponentDiagram {
        // C4 Model
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "SupportNotificationsComponent";

        // Components
        public Component NotificationController { get; private set; }
        public Component SupportController { get; private set; }
        public Component NotificationService { get; private set; }
        public Component SupportService { get; private set; }
        public Component NotificationRepository { get; private set; }
        public Component SupportRepository { get; private set; }
        public Component NotificationEntity { get; private set; }
        public Component SupportTicketEntity { get; private set; }

        // Constructor
        public SupportNotificationsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram) {
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
            NotificationController = containerDiagram.RestApi.AddComponent(
                "Notification Controller",
                "Maneja solicitudes de envío de notificaciones (SMS, email, push).",
                "ASP.NET Core Controller"
            );

            SupportController = containerDiagram.RestApi.AddComponent(
                "Support Controller",
                "Maneja solicitudes de soporte técnico e integra con Zendesk.",
                "ASP.NET Core Controller"
            );

            NotificationService = containerDiagram.RestApi.AddComponent(
                "Notification Service",
                "Lógica de negocio para enviar notificaciones usando Twilio y SendGrid.",
                "C# Service"
            );

            SupportService = containerDiagram.RestApi.AddComponent(
                "Support Service",
                "Lógica de negocio para registrar y gestionar tickets de soporte vía Zendesk.",
                "C# Service"
            );

            NotificationRepository = containerDiagram.RestApi.AddComponent(
                "Notification Repository",
                "Persistencia de historial de notificaciones enviadas.",
                "C# Repository"
            );

            SupportRepository = containerDiagram.RestApi.AddComponent(
                "Support Repository",
                "Persistencia de tickets de soporte y estados.",
                "C# Repository"
            );

            NotificationEntity = containerDiagram.RestApi.AddComponent(
                "Notification Entity",
                "Entidad de dominio que representa una notificación con datos como tipo, mensaje, canal, fecha.",
                "C# Class"
            );

            SupportTicketEntity = containerDiagram.RestApi.AddComponent(
                "Support Ticket Entity",
                "Entidad de dominio que representa un ticket de soporte con atributos como Id, estado, descripción, usuario.",
                "C# Class"
            );
        }

        // Add Relationships
        private void AddRelationships() {
            // People to Components
            contextDiagram.Jefe_de_Hogar.Uses(
                NotificationController, 
                "Recibe notificaciones de alertas y consumo"
            );
            contextDiagram.Jefe_de_Hogar.Uses(
                SupportController, 
                "Crea tickets de soporte desde la app"
            );

            contextDiagram.Maestro_Cervecero.Uses(
                NotificationController, 
                "Recibe notificaciones de variables críticas de producción"
            );
            contextDiagram.Maestro_Cervecero.Uses(
                SupportController, 
                "Crea tickets de soporte técnico"
            );

            // Components to Components
            NotificationController.Uses(NotificationService, "Delegación de lógica de envío de notificaciones");
            SupportController.Uses(SupportService, "Delegación de lógica de soporte");

            NotificationService.Uses(NotificationRepository, "Gestión de historial de notificaciones");
            SupportService.Uses(SupportRepository, "Gestión de tickets de soporte");

            NotificationRepository.Uses(NotificationEntity, "Mapea datos entre DB y modelo de dominio");
            SupportRepository.Uses(SupportTicketEntity, "Mapea datos entre DB y modelo de dominio");

            // Components to Containers
            NotificationRepository.Uses(containerDiagram.Database, "Lee y escribe historial de notificaciones", "SQL");
            SupportRepository.Uses(containerDiagram.Database, "Lee y escribe tickets de soporte", "SQL");

            // Components to External Systems
            NotificationService.Uses(contextDiagram.Twilio, "Envía notificaciones SMS/WhatsApp", "JSON/HTTPS");
            SupportService.Uses(contextDiagram.Zendesk, "Registra y consulta tickets de soporte", "REST API");
        }

        // Apply Styles 
        private void ApplyStyles() {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;
           
            // Components
            styles.Add(new ElementStyle(componentTag) {
                Background = "#ef6c00   ", // morado oscuro para soporte & notificaciones
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags() {
            NotificationController.AddTags(componentTag);
            SupportController.AddTags(componentTag);
            NotificationService.AddTags(componentTag);
            SupportService.AddTags(componentTag);
            NotificationRepository.AddTags(componentTag);
            SupportRepository.AddTags(componentTag);
            NotificationEntity.AddTags(componentTag);
            SupportTicketEntity.AddTags(componentTag);
        }

        // Create View
        private void CreateView() {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi,
                "droplet-component-support-notifications",
                "Component Diagram - Support & Notifications Bounded Context"
            );

            // Title
            componentView.Title = "Droplet - Support & Notifications";
            
            // Elements
            componentView.Add(NotificationController);
            componentView.Add(SupportController);
            componentView.Add(NotificationService);
            componentView.Add(SupportService);
            componentView.Add(NotificationRepository);
            componentView.Add(SupportRepository);
            componentView.Add(NotificationEntity);
            componentView.Add(SupportTicketEntity);

            // People
            componentView.Add(contextDiagram.Jefe_de_Hogar);
            componentView.Add(contextDiagram.Maestro_Cervecero);

            // Database
            componentView.Add(containerDiagram.Database);

            // External Systems
            componentView.Add(contextDiagram.Twilio);
            componentView.Add(contextDiagram.Zendesk);
        }
    }
}
