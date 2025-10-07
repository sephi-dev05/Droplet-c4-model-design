using Structurizr;

namespace Droplet_c4_model_design {
    public class ServiceMonitoringAlertsComponentDiagram {
        // C4 Model
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "MonitoringAlertsComponent";

        // Components
        public Component MonitoringController { get; private set; }
        public Component AlertsController { get; private set; }
        public Component MonitoringService { get; private set; }
        public Component AlertsService { get; private set; }
        public Component MonitoringRepository { get; private set; }
        public Component MonitoringEntity { get; private set; }

        // Constructor
        public ServiceMonitoringAlertsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram) {
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
            MonitoringController = containerDiagram.RestApi.AddComponent(
                "Monitoring Controller",
                "Expone endpoints para recibir y consultar datos de sensores IoT (nivel, caudal, temperatura, densidad).",
                "ASP.NET Core Controller"
            );

            AlertsController = containerDiagram.RestApi.AddComponent(
                "Alerts Controller",
                "Maneja solicitudes relacionadas con la generación, consulta y configuración de alertas.",
                "ASP.NET Core Controller"
            );

            MonitoringService = containerDiagram.RestApi.AddComponent(
                "Monitoring Service",
                "Procesa los datos recibidos de sensores IoT y actualiza métricas de consumo o producción.",
                "C# Service"
            );

            AlertsService = containerDiagram.RestApi.AddComponent(
                "Alerts Service",
                "Aplica reglas de negocio para generar alertas críticas (fugas, bajo nivel, anomalías en fermentación).",
                "C# Service"
            );

            MonitoringRepository = containerDiagram.RestApi.AddComponent(
                "Monitoring Repository",
                "Persistencia de datos de sensores, métricas y eventos de alerta.",
                "C# Repository"
            );

            MonitoringEntity = containerDiagram.RestApi.AddComponent(
                "Monitoring Entity",
                "Entidad de dominio que representa los datos capturados: nivel, caudal, temperatura, densidad, estado de alerta.",
                "C# Class"
            );
        }

        // Add Relationships
        private void AddRelationships() {
            // People to Components
            contextDiagram.Jefe_de_Hogar.Uses(
                MonitoringController, 
                "Consulta el nivel de agua y consumo en tiempo real"
            );
            contextDiagram.Jefe_de_Hogar.Uses(
                AlertsController, 
                "Recibe alertas de bajo nivel, fugas y predicciones"
            );

            contextDiagram.Maestro_Cervecero.Uses(
                MonitoringController, 
                "Consulta variables críticas de fermentación en tiempo real"
            );
            contextDiagram.Maestro_Cervecero.Uses(
                AlertsController, 
                "Recibe alertas de desviaciones en temperatura/densidad"
            );

            // Components to Components
            MonitoringController.Uses(MonitoringService, "Invoca para procesar datos de sensores");
            AlertsController.Uses(AlertsService, "Invoca para procesar reglas de alertas");

            MonitoringService.Uses(MonitoringRepository, "Guarda y consulta métricas de sensores");
            AlertsService.Uses(MonitoringRepository, "Consulta datos para generar alertas");

            MonitoringRepository.Uses(MonitoringEntity, "Mapea datos entre DB y modelo de dominio");

            // Components to Containers
            MonitoringRepository.Uses(
                containerDiagram.Database, 
                "Lee y escribe datos de sensores y alertas", 
                "SQL"
            );

            // API to External Systems (Sensores IoT → API REST)
            containerDiagram.RestApi.Uses(
                contextDiagram.IoTDevices,
                "Recibe datos en tiempo real desde sensores IoT (nivel, caudal, temperatura, densidad)",
                "MQTT/HTTP"
            );

        }

        // Apply Styles 
        private void ApplyStyles() {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;
           
            // Components
            styles.Add(new ElementStyle(componentTag) {
                Background = "#d32f2f", // rojo para alertas
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags() {
            MonitoringController.AddTags(componentTag);
            AlertsController.AddTags(componentTag);
            MonitoringService.AddTags(componentTag);
            AlertsService.AddTags(componentTag);
            MonitoringRepository.AddTags(componentTag);
            MonitoringEntity.AddTags(componentTag);
        }

        // Create View
        private void CreateView() {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi,
                "droplet-component-monitoring-alerts",
                "Component Diagram - Monitoring & Alerts Bounded Context"
            );

            // Title
            componentView.Title = "Droplet - Monitoring & Alerts";
            
            // Elements
            componentView.Add(MonitoringController);
            componentView.Add(AlertsController);
            componentView.Add(MonitoringService);
            componentView.Add(AlertsService);
            componentView.Add(MonitoringRepository);
            componentView.Add(MonitoringEntity);

            // People
            componentView.Add(contextDiagram.Jefe_de_Hogar);
            componentView.Add(contextDiagram.Maestro_Cervecero);

            // Database
            componentView.Add(containerDiagram.Database);
        }
    }
}
