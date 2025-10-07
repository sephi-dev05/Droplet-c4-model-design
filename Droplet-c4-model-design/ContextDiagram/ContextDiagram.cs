using Structurizr;

namespace Droplet_c4_model_design {
    public class ContextDiagram {
        // C4 Model
        private readonly C4 c4;
        
        // Software Systems
        public SoftwareSystem Droplet { get; private set; }
        public SoftwareSystem Zendesk  { get; private set; }
        public SoftwareSystem IoTDevices { get; private set; }
        public SoftwareSystem PayPal  { get; private set; }
        public SoftwareSystem Twilio  { get; private set; }

        
        // People
        public Person Jefe_de_Hogar { get; private set; }
        public Person Maestro_Cervecero { get; private set; }
        public Person Fluix_Administrator { get; private set; }

        // Constructor
        public ContextDiagram(C4 c4) {
            this.c4 = c4;
        }
        
        // Generate Method
        public void Generate() {
            AddElements();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }
        
        // Add Elements
        private void AddElements() {
            AddPeople();
            AddSoftwareSystems();
        }

        // Add People
        private void AddPeople() {
            Jefe_de_Hogar = c4.Model.AddPerson(
                "Jefe de Hogar", 
                "Usuario residencial que monitorea su cisterna/tanque y recibe alertas de consumo y fugas."
            );

            Maestro_Cervecero = c4.Model.AddPerson(
                "Maestro Cervecero", 
                "Usuario pyme que monitorea fermentación, insumos críticos y genera reportes de calidad."
            );
            
            Fluix_Administrator = c4.Model.AddPerson(
                "Admin Sistema", 
                "Administra la plataforma Droplet, gestiona usuarios, suscripciones y supervisa el sistema."
            );
        }
        
        // Add Software Systems
        private void AddSoftwareSystems() {
            Droplet = c4.Model.AddSoftwareSystem(
                "Droplet", 
                "Plataforma IoT + Web/App para monitoreo de líquidos."
            );

            IoTDevices = c4.Model.AddSoftwareSystem(
                "IoT Devices",
                "Sensores de nivel, caudal, temperatura y densidad que envían datos en tiempo real."
            );

            Zendesk = c4.Model.AddSoftwareSystem(
                "Zendesk", 
                "Soporte técnico."
                );
            
            PayPal = c4.Model.AddSoftwareSystem(
                "PayPal", 
                "Pasarela de pagos para suscripciones."
                );
            
            Twilio = c4.Model.AddSoftwareSystem(
                "Twilio", 
                "Notificaciones SMS/WhatsApp."
                );
        }

        // Add Relationships
        private void AddRelationships() {
            // People to Droplet
            Jefe_de_Hogar.Uses(Droplet, "Consulta dashboard, recibe alertas y paga suscripción.");
            Maestro_Cervecero.Uses(Droplet, "Monitorea fermentación, consulta reportes y recibe alertas.");
            Fluix_Administrator.Uses(Droplet, "Administra usuarios, suscripciones y supervisa el sistema.");
            
            // Droplet to External Systems
            Droplet.Uses(Zendesk, "Provee soporte técnico a los usuarios.");
            Droplet.Uses(IoTDevices, "Recibe datos en tiempo real de sensores IoT.");
            Droplet.Uses(PayPal, "Procesa pagos de suscripciones.");
            Droplet.Uses(Twilio, "Envía notificaciones SMS/WhatsApp.");
        }

        // Apply Styles
        private void ApplyStyles() {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;
            
            // People
            styles.Add(new ElementStyle(nameof(Jefe_de_Hogar)) {
                Background = "#1976d2", 
                Color = "#ffffff", 
                Shape = Shape.Person
            });
            
            styles.Add(new ElementStyle(nameof(Maestro_Cervecero)) {
                Background = "#8e24aa", 
                Color = "#ffffff", 
                Shape = Shape.Person
            });
            
            styles.Add(new ElementStyle(nameof(Fluix_Administrator)) {
                Background = "#d32f2f", 
                Color = "#ffffff", 
                Shape = Shape.Person
            });

            // Software Systems
            styles.Add(new ElementStyle(nameof(Droplet)) {
                Background = "#2e7d32", 
                Color = "#ffffff", 
                Shape = Shape.RoundedBox
            });
            
            styles.Add(new ElementStyle(nameof(Zendesk)) {
                Background = "#4D1D6E", 
                Color = "#ffffff", 
                Shape = Shape.RoundedBox
            });
            
            styles.Add(new ElementStyle(nameof(IoTDevices)) {
                Background = "#f57c00", 
                Color = "#ffffff", 
                Shape = Shape.RoundedBox
            });
            
            styles.Add(new ElementStyle(nameof(PayPal)) {
                Background = "#19ACFA", 
                Color = "#ffffff", 
                Shape = Shape.RoundedBox
            });
            
            styles.Add(new ElementStyle(nameof(Twilio)) {
                Background = "#631818", 
                Color = "#ffffff", 
                Shape = Shape.RoundedBox
            });
        }
        
        // Set Tags
        private void SetTags() {
            // People
            Jefe_de_Hogar.AddTags(nameof(Jefe_de_Hogar));
            Maestro_Cervecero.AddTags(nameof(Maestro_Cervecero));
            Fluix_Administrator.AddTags(nameof(Fluix_Administrator));
            
            // Software Systems
            Droplet.AddTags(nameof(Droplet));
            Zendesk.AddTags(nameof(Zendesk));
            IoTDevices.AddTags(nameof(IoTDevices));
            PayPal.AddTags(nameof(PayPal));
            Twilio.AddTags(nameof(Twilio));
        }

        // Create View
        private void CreateView() {
            SystemContextView contextView = c4.ViewSet.CreateSystemContextView(
                Droplet, 
                "droplet-context", 
                "Context Diagram - Droplet System Context"
            );
            
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();
        }
    }
}
