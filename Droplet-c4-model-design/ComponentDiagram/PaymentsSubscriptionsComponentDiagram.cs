using Structurizr;

namespace Droplet_c4_model_design
{
    public class PaymentsSubscriptionsComponentDiagram
    {
        // C4 Model
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "BillingSubscriptionComponent";

        // Components
        public Component PaymentController { get; private set; }
        public Component PaymentService { get; private set; }
        public Component SubscriptionRepository { get; private set; }
        public Component SubscriptionEntity { get; private set; }
        public Component PaypalAdapter { get; private set; }

        // Constructor
        public PaymentsSubscriptionsComponentDiagram(C4 c4, ContextDiagram contextDiagram,
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
            PaymentController = containerDiagram.RestApi.AddComponent(
                "Payment Controller",
                "Expone endpoints para procesar pagos y generar facturas.",
                "ASP.NET Core Controller"
            );

            PaymentService = containerDiagram.RestApi.AddComponent(
                "Payment Service",
                "Expone endpoints para procesar pagos.",
                "ASP.NET Core Service"
            );

            SubscriptionRepository = containerDiagram.RestApi.AddComponent(
                "Subscription Repository",
                "Persistencia de planes activos, historial de suscripciones y renovaciones.",
                "C# Repository"
            );

            SubscriptionEntity = containerDiagram.RestApi.AddComponent(
                "Subscription Entity",
                "Entidad de dominio que representa la suscripción de un usuario (plan, estado, vigencia).",
                "C# Class"
            );

            PaypalAdapter = containerDiagram.RestApi.AddComponent(
                "Paypal Adapter",
                "Lógica de negocio para validar pagos, generar facturas y gestionar integración con PayPal.",
                "C# Service"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // People to Components
            contextDiagram.Jefe_de_Hogar.Uses(
                PaymentController,
                "Realiza pagos y consulta facturas"
            );

            contextDiagram.Maestro_Cervecero.Uses(
                PaymentController,
                "Realiza pagos y revisa facturación"
            );

            // Components to Components
            PaymentController.Uses(
                PaymentService,
                "Delegación de lógica de facturación"
            );

            PaymentService.Uses(
                SubscriptionRepository,
                ""
            );

            SubscriptionRepository.Uses(
                SubscriptionEntity,
                "Mapea datos de suscripciones"
            );

            SubscriptionRepository.Uses(
                containerDiagram.Database,
                "Lee y escribe datos de suscripciones",
                "SQL"
            );

            
            // Components to External Systems
            PaymentService.Uses(
                PaypalAdapter,
                ""
            );
            
            PaypalAdapter.Uses(
                contextDiagram.PayPal,
                "Procesa pagos de suscripciones",
                "JSON/HTTPS"
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
                Background = "#19ACFA", // celeste para billing & subscription
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags()
        {
            PaymentController.AddTags(componentTag);
            PaymentService.AddTags(componentTag);
            SubscriptionRepository.AddTags(componentTag);
            SubscriptionEntity.AddTags(componentTag);
            PaypalAdapter.AddTags(componentTag);
        }

        // Create View
        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.RestApi,
                "droplet-component-payments-subscriptions",
                "Component Diagram - Payments & Subscriptions Bounded Context"
            );

            // Title
            componentView.Title = "Droplet - Payments & Subscriptions";

            // Elements
            componentView.Add(PaymentController);
            componentView.Add(PaymentService);
            componentView.Add(SubscriptionRepository);
            componentView.Add(SubscriptionEntity);
            componentView.Add(PaypalAdapter);

            // People
            componentView.Add(contextDiagram.Jefe_de_Hogar);
            componentView.Add(contextDiagram.Maestro_Cervecero);

            // Database
            componentView.Add(containerDiagram.Database);

            // External System
            componentView.Add(contextDiagram.PayPal);
        }
    }
}