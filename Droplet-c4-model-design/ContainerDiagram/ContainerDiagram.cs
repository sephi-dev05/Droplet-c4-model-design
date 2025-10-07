using Structurizr;

namespace Droplet_c4_model_design
{
    public class ContainerDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;

        // Containers
        public Container MobileApplication { get; private set; }
        public Container LandingPage { get; private set; }
        public Container WebApplication { get; private set; }
        public Container RestApi { get; private set; }
        public Container Database { get; private set; }
        public Container IdentityAccess { get; private set; }


        // Constructor
        public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
        }

        // Generate Method
        public void Generate()
        {
            AddContainers();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Containers
        private void AddContainers()
        {
            MobileApplication = contextDiagram.Droplet.AddContainer(
                "Mobile App",
                "Aplicación móvil que ofrece acceso rápido al dashboard, alertas y notificaciones.",
                "Flutter"
            );

            LandingPage = contextDiagram.Droplet.AddContainer(
                "Landing Page",
                "Página pública de Droplet. Presenta la propuesta de valor, planes de suscripción y acceso a la app.",
                "HTML5, CSS3, JavaScript"
            );

            WebApplication = contextDiagram.Droplet.AddContainer(
                "Web Application",
                "Aplicación web principal usada por hogares y pymes. Permite visualizar el dashboard, configurar alertas, revisar reportes, gestionar pagos, inventario de tanques y soporte.",
                "Angular"
            );

            RestApi = contextDiagram.Droplet.AddContainer(
                "API REST",
                "Backend central que expone la lógica de negocio y conecta sensores IoT con la app. Implementa los bounded contexts: Monitoring, Notifications, Billing, Brewery.",
                "Spring Boot (Java) + Swagger/OpenAPI"
            );

            Database = contextDiagram.Droplet.AddContainer(
                "SQL Database",
                "Base de datos relacional que almacena usuarios, sensores, consumos, reportes, alertas y suscripciones.",
                "SQL Server"
            );

            IdentityAccess = contextDiagram.Droplet.AddContainer(
                "Identity & Access Management",
                "Módulo del backend encargado de autenticación, autorización y gestión de usuarios. Implementa login, registro y emisión de tokens JWT.",
                "Spring Boot"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // People to Containers
            contextDiagram.Jefe_de_Hogar.Uses(
                WebApplication,
                "Monitorea tanque, recibe alertas, revisa historial de consumo y paga suscripción."
            );

            contextDiagram.Jefe_de_Hogar.Uses(
                MobileApplication,
                "Accede al dashboard y recibe notificaciones en tiempo real."
            );

            contextDiagram.Jefe_de_Hogar.Uses(
                LandingPage,
                "Accede a la landing para conocer planes y registrarse."
            );


            contextDiagram.Maestro_Cervecero.Uses(
                WebApplication,
                "Monitorea fermentación, genera reportes de lotes y controla insumos críticos."
            );

            contextDiagram.Maestro_Cervecero.Uses(
                MobileApplication,
                "Accede al dashboard, consulta variables críticas y recibe alertas."
            );

            contextDiagram.Maestro_Cervecero.Uses(
                LandingPage,
                "Accede a la landing para suscribirse y registrarse."
            );


            contextDiagram.Fluix_Administrator.Uses(
                WebApplication,
                "Gestiona usuarios, suscripciones y supervisa el sistema."
            );

            contextDiagram.Fluix_Administrator.Uses(
                LandingPage,
                "Gestiona contenido y planes de suscripción."
            );

            contextDiagram.Fluix_Administrator.Uses(
                MobileApplication,
                "Supervisa notificaciones y alertas del sistema."
            );

            // Containers to Containers
            WebApplication.Uses(
                RestApi,
                "Consume API para datos de consumo, alertas, pagos y reportes",
                "JSON/HTTPS"
            );

            MobileApplication.Uses(
                RestApi,
                "Consume API para monitoreo y notificaciones",
                "JSON/HTTPS"
            );

            // Containers to Software System
            RestApi.Uses(
                Database,
                "Lee y escribe información",
                "JDBC/JPA"
            );

            RestApi.Uses(
                IdentityAccess,
                "Validates user credentials and roles",
                "JSON/HTTPS"
            );
            
            RestApi.Uses(
                contextDiagram.PayPal,
                "Procesa pagos y suscripciones",
                "JSON/HTTPS"
            );

            RestApi.Uses(
                contextDiagram.Twilio,
                "Envía notificaciones SMS/WhatsApp",
                "JSON/HTTPS"
            );

            RestApi.Uses(
                contextDiagram.Zendesk,
                "Gestiona soporte técnico y tickets",
                "REST API"
            );

            RestApi.Uses(
                contextDiagram.IoTDevices,
                "Recibe datos en tiempo real de sensores IoT",
                "MQTT/HTTPS"
            );
        }

        // Apply Styles
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            // Containers
            styles.Add(new ElementStyle(nameof(LandingPage))
            {
                Background = "#ef6c00",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(WebApplication))
            {
                Background = "#1565c0",
                Color = "#ffffff",
                Shape = Shape.WebBrowser
            });

            styles.Add(new ElementStyle(nameof(MobileApplication))
            {
                Background = "#009688",
                Color = "#ffffff",
                Shape = Shape.MobileDevicePortrait
            });

            styles.Add(new ElementStyle(nameof(RestApi))
            {
                Background = "#455a64",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(Database))
            {
                Background = "#303f9f",
                Color = "#ffffff",
                Shape = Shape.Cylinder
            });

            styles.Add(new ElementStyle(nameof(IdentityAccess))
            {
                Background = "#6a1b9a",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });
        }

        // Set Tags
        private void SetTags()
        {
            MobileApplication.AddTags(nameof(MobileApplication));
            LandingPage.AddTags(nameof(LandingPage));
            WebApplication.AddTags(nameof(WebApplication));
            RestApi.AddTags(nameof(RestApi));
            Database.AddTags(nameof(Database));
            IdentityAccess.AddTags(nameof(IdentityAccess));
        }

        // Create View
        private void CreateView()
        {
            ContainerView containerView = c4.ViewSet.CreateContainerView(
                contextDiagram.Droplet,
                "droplet-container",
                "Container Diagram - Droplet System"
            );

            containerView.AddAllElements();
        }
    }
}