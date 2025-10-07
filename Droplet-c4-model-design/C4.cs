using Structurizr;
using Structurizr.Api;

namespace Droplet_c4_model_design {
	public class C4 {
		// Structurizr configuration
		private readonly long workspaceId = 106839;
		private readonly string apiKey = "baf1bf77-1966-467a-8bbe-0809af9814b9";
		private readonly string apiSecret = "56554b11-a8ea-4edc-9761-2cef8b047727";

		// C4 Model
		public StructurizrClient StructurizrClient { get; }
		public Workspace Workspace { get; }
		public Model Model { get; }
		public ViewSet ViewSet { get; }

		// Constructor
		public C4() {
			// Initialize Structurizr client and workspace
			string workspaceName = "Droplet - DDD";
			string workspaceDescription = "Domain-Driven Software Architecture for Droplet application.";
			
			StructurizrClient = new StructurizrClient(apiKey, apiSecret);
			Workspace = new Workspace(workspaceName, workspaceDescription);
			Model = Workspace.Model;
			ViewSet = Workspace.Views;
		}

		// Generate Method
		public void Generate() {
			// Create diagrams
			ContextDiagram contextDiagram = new ContextDiagram(this);
			ContainerDiagram containerDiagram = new ContainerDiagram(this, contextDiagram);
			IdentityAccessComponentDiagram identityAccessComponentDiagram = new IdentityAccessComponentDiagram(this, contextDiagram, containerDiagram);
			ProfilesPreferencesComponentDiagram profilesPreferencesComponentDiagram = new ProfilesPreferencesComponentDiagram(this, contextDiagram, containerDiagram);
			ServiceMonitoringAlertsComponentDiagram serviceMonitoringAlertsComponentDiagram = new ServiceMonitoringAlertsComponentDiagram(this, contextDiagram, containerDiagram);
			DashboardAnalyticsComponentDiagram dashboardAnalyticsComponentDiagram = new DashboardAnalyticsComponentDiagram(this, contextDiagram, containerDiagram);
			ConsumptionInventoryComponentDiagram consumptionInventoryComponentDiagram = new ConsumptionInventoryComponentDiagram(this, contextDiagram, containerDiagram);
			SystemAdministrationComponentDiagram systemAdministrationComponentDiagram = new SystemAdministrationComponentDiagram(this, contextDiagram, containerDiagram);
			PaymentsSubscriptionsComponentDiagram paymentsSubscriptionsComponentDiagram = new PaymentsSubscriptionsComponentDiagram(this, contextDiagram, containerDiagram);
			SupportNotificationsComponentDiagram supportNotificationsComponentDiagram = new SupportNotificationsComponentDiagram(this, contextDiagram, containerDiagram);
			
			// Generate diagrams
			contextDiagram.Generate();
			containerDiagram.Generate();
			identityAccessComponentDiagram.Generate();
			profilesPreferencesComponentDiagram.Generate();
			serviceMonitoringAlertsComponentDiagram.Generate();
			dashboardAnalyticsComponentDiagram.Generate();
			consumptionInventoryComponentDiagram.Generate();
			systemAdministrationComponentDiagram.Generate();
			paymentsSubscriptionsComponentDiagram.Generate();
			supportNotificationsComponentDiagram.Generate();
			
			// Upload workspace to Structurizr
			StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}