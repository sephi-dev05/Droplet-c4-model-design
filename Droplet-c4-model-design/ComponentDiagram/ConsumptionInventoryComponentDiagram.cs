using Structurizr;

namespace Droplet_c4_model_design {
    public class ConsumptionInventoryComponentDiagram {
        // C4 Model
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "ConsumptionInventoryComponent";

        // Components
        public Component ConsumptionController { get; private set; }
        public Component InventoryController { get; private set; }
        public Component ConsumptionService { get; private set; }
        public Component InventoryService { get; private set; }
        public Component ConsumptionRepository { get; private set; }
        public Component InventoryRepository { get; private set; }
        public Component ConsumptionEntity { get; private set; }
        public Component InventoryEntity { get; private set; }

        // Constructor
        public ConsumptionInventoryComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram) {
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
            ConsumptionController = containerDiagram.RestApi.AddComponent(
                "Consumption Controller",
                "Maneja solicitudes de visualización del consumo histórico y predicciones de agotamiento del tanque.",
                "ASP.NET Core Controller"
            );

            InventoryController = containerDiagram.RestApi.AddComponent(
                "Inventory Controller",
                "Maneja solicitudes de gestión de inventario de tanques (hogares) e insumos líquidos (pymes).",
                "ASP.NET Core Controller"
            );

            ConsumptionService = containerDiagram.RestApi.AddComponent(
                "Consumption Service",
                "Aplica reglas de negocio para analizar consumo, generar métricas y predicciones de reabastecimiento.",
                "C# Service"
            );

            InventoryService = containerDiagram.RestApi.AddComponent(
                "Inventory Service",
                "Lógica de negocio para gestionar el inventario de tanques e insumos líquidos en pymes.",
                "C# Service"
            );

            ConsumptionRepository = containerDiagram.RestApi.AddComponent(
                "Consumption Repository",
                "Persistencia de datos históricos de consumo de agua y métricas asociadas.",
                "C# Repository"
            );

            InventoryRepository = containerDiagram.RestApi.AddComponent(
                "Inventory Repository",
                "Persistencia de inventario de tanques (residencial) e insumos líquidos (pymes).",
                "C# Repository"
            );

            ConsumptionEntity = containerDiagram.RestApi.AddComponent(
                "Consumption Entity",
                "Entidad de dominio con datos de consumo de agua: nivel histórico, predicciones de agotamiento.",
                "C# Class"
            );

            InventoryEntity = containerDiagram.RestApi.AddComponent(
                "Inventory Entity",
                "Entidad de dominio con datos de inventario: insumos, cantidad disponible, fechas de reposición.",
                "C# Class"
            );
        }

        // Add Relationships
        private void AddRelationships() {
            // People to Components
            contextDiagram.Jefe_de_Hogar.Uses(
                ConsumptionController,
                "Consulta historial de consumo y predicciones de reabastecimiento"
            );
            contextDiagram.Jefe_de_Hogar.Uses(
                InventoryController,
                "Gestiona inventario de agua almacenada en su tanque"
            );

            contextDiagram.Maestro_Cervecero.Uses(
                ConsumptionController,
                "Consulta métricas de consumo de agua en producción"
            );
            contextDiagram.Maestro_Cervecero.Uses(
                InventoryController,
                "Gestiona inventario de insumos líquidos para fermentación"
            );

            // Components to Components
            ConsumptionController.Uses(ConsumptionService, "Delegación de lógica de consumo");
            InventoryController.Uses(InventoryService, "Delegación de lógica de inventario");

            ConsumptionService.Uses(ConsumptionRepository, "Gestión de datos de consumo");
            InventoryService.Uses(InventoryRepository, "Gestión de datos de inventario");

            ConsumptionRepository.Uses(ConsumptionEntity, "Mapea datos entre DB y modelo de dominio");
            InventoryRepository.Uses(InventoryEntity, "Mapea datos entre DB y modelo de dominio");

            // Components to Containers
            ConsumptionRepository.Uses(containerDiagram.Database, "Lee y escribe datos de consumo", "SQL");
            InventoryRepository.Uses(containerDiagram.Database, "Lee y escribe datos de inventario", "SQL");
        }

        // Apply Styles 
        private void ApplyStyles() {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;
           
            // Components
            styles.Add(new ElementStyle(componentTag) {
                Background = "#fbc02d", // azul para consumo/inventario
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags() {
            ConsumptionController.AddTags(componentTag);
            InventoryController.AddTags(componentTag);
            ConsumptionService.AddTags(componentTag);
            InventoryService.AddTags(componentTag);
            ConsumptionRepository.AddTags(componentTag);
            InventoryRepository.AddTags(componentTag);
            ConsumptionEntity.AddTags(componentTag);
            InventoryEntity.AddTags(componentTag);
        }

        // Create View
        private void CreateView() {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi,
                "droplet-component-consumption-inventory",
                "Component Diagram - Consumption & Inventory Bounded Context"
            );

            // Title
            componentView.Title = "Droplet - Consumption & Inventory";
            
            // Elements
            componentView.Add(ConsumptionController);
            componentView.Add(InventoryController);
            componentView.Add(ConsumptionService);
            componentView.Add(InventoryService);
            componentView.Add(ConsumptionRepository);
            componentView.Add(InventoryRepository);
            componentView.Add(ConsumptionEntity);
            componentView.Add(InventoryEntity);

            // People
            componentView.Add(contextDiagram.Jefe_de_Hogar);
            componentView.Add(contextDiagram.Maestro_Cervecero);

            // Database
            componentView.Add(containerDiagram.Database);
        }
    }
}
