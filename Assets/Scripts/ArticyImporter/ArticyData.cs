using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    /// <summary>
    /// Represents Articy data in a schema-independent format for dialogue systems.
    /// This class is designed to hold data extracted from Articy:draft XML exports,
    /// allowing the dialogue system to process and utilize the data effectively.
    /// </summary>
    public class ArticyData
    {
        /// <summary>
        /// Represents a text that can be localized into multiple languages.
        /// Contains a dictionary mapping language codes to their respective translations.
        /// </summary>
        public class LocalizableText
        {
            // Dictionary to store localized strings, using language codes as keys.
            public Dictionary<string, string> localizedString = new Dictionary<string, string>();

            /// <summary>
            /// Gets the default text for the current LocalizableText instance.
            /// Tries to retrieve a fallback order: first empty key, then 'en', then any available text.
            /// </summary>
            public string DefaultText 
            { 
                get 
                {
                    string text;
                    // Try to get text for the default language (empty key).
                    if (localizedString.TryGetValue(string.Empty, out text) && !string.IsNullOrEmpty(text)) return text;
                    // Fallback to English if default is not available.
                    if (localizedString.TryGetValue("en", out text) && !string.IsNullOrEmpty(text)) return text;
                    // Fallback to any available text if English is not available.
                    foreach (var kvp in localizedString)
                    {
                        if (!string.IsNullOrEmpty(kvp.Value)) return kvp.Value;
                    }
                    // Return empty string if no valid text is found.
                    return string.Empty;
                } 
            }
        }
        
        /// <summary>
        /// Represents an element within the Articy data structure.
        /// An element could be a dialogue node, character, or any other unit of content.
        /// </summary>
        public class Element
        {
            // Unique identifier for the element.
            public string id;
            // Technical name used for internal reference.
            public string technicalName;
            // Display name, supporting localization.
            public LocalizableText displayName;
            // Main text content of the element, supporting localization.
            public LocalizableText text;
            // Features associated with the element, containing additional properties.
            public Features features;
            // Position of the element, potentially used for visualization/layout purposes.
            public Vector2 position;

            /// <summary>
            /// Default constructor initializing fields with default values.
            /// </summary>
            public Element()
            {
                id = string.Empty;
                technicalName = string.Empty;
                displayName = new LocalizableText();
                text = new LocalizableText();
                features = new Features();
                position = Vector2.zero;
            }

            /// <summary>
            /// Parameterized constructor for creating an element with specified values.
            /// </summary>
            /// <param name="id">Element's unique identifier.</param>
            /// <param name="technicalName">Technical name for internal reference.</param>
            /// <param name="displayName">Localized display name.</param>
            /// <param name="text">Localized text content.</param>
            /// <param name="features">Features associated with the element.</param>
            /// <param name="position">Position of the element.</param>
            public Element(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position)
            {
                this.id = id;
                this.technicalName = technicalName;
                this.displayName = displayName;
                this.text = text;
                this.features = features;
                this.position = position;
            }
        }
        
        /// <summary>
        /// Represents a collection of features associated with an element.
        /// Features can include additional attributes or properties of the content.
        /// </summary>
        public class Features
        {
            // List of features, each containing its own properties.
            private List<Feature> m_features;

            /// <summary>
            /// Default constructor initializing an empty list of features.
            /// </summary>
            public Features()
            {
                m_features = new List<Feature>();
            }

            /// <summary>
            /// Parameterized constructor for creating a features collection with specified features.
            /// </summary>
            /// <param name="features">List of features to initialize with.</param>
            public Features(List<Feature> features)
            {
                this.m_features = features;
            }
        }
        
        /// <summary>
        /// Represents a single feature, which is a component of an element's features.
        /// A feature usually has a name and a list of properties.
        /// </summary>
        public class Feature
        {
            // Name of the feature for identification purposes.
            public string Name;
            // List of properties associated with this feature.
            public List<Property> Properties;

            /// <summary>
            /// Default constructor initializing an empty list of properties.
            /// </summary>
            public Feature()
            {
                Properties = new List<Property>();
            }

            /// <summary>
            /// Parameterized constructor for creating a feature with specified properties.
            /// </summary>
            /// <param name="properties">List of properties to initialize with.</param>
            public Feature(List<Property> properties)
            {
                this.Properties = properties;
            }
        }
        
        /// <summary>
        /// Represents a property within a feature.
        /// A property can consist of multiple fields defining specific attributes.
        /// </summary>
        public class Property
        {
            // List of fields that make up this property.
            public List<Field> Fields;

            /// <summary>
            /// Default constructor initializing an empty list of fields.
            /// </summary>
            public Property()
            {
                Fields = new List<Field>();
            }

            /// <summary>
            /// Parameterized constructor for creating a property with specified fields.
            /// </summary>
            /// <param name="fields">List of fields to initialize with.</param>
            public Property(List<Field> fields)
            {
                this.Fields = fields;
            }
        }
        
        /// <summary>
        /// Represents this project information.
        /// </summary>
        public class Project
        {
            
            public string DisplayName = string.Empty;
            public string CreatedTime = string.Empty;
            public string CreatorTool = string.Empty;
            public string CreatorVersion = string.Empty;
        }
        
        public class Asset : Element
        {
            public string AssetFilename;
            
            public Asset(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position, string assetFilename)
                : base(id, technicalName, displayName, text, features, position)
            {
                this.AssetFilename = assetFilename;
            }
        }
        
        public class Entity : Element
        {
            public string PreviewImage;

            public Entity(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position, string previewImage)
                : base(id, technicalName, displayName, text, features, position)
            {
                this.PreviewImage = previewImage;
            }
        }
        
        public class Location : Element
        {
            public Location(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position)
                : base(id, technicalName, displayName, text, features, position) { }
        }
        
        public class FlowFragment : Element
        {
            public List<Pin> pins;

            public FlowFragment()
                : base()
            {
                pins = new List<Pin>();
            }

            public FlowFragment(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position, List<Pin> pins)
                : base(id, technicalName, displayName, text, features, position)
            {
                this.pins = pins;
            }
        }
        
        public enum SemanticType { Input, Output };
        public class Pin
        {
            public string ID;
            public int Index;
            public SemanticType Semantic;
            public string Expression;
            
            public Pin(string id, int index, SemanticType semantic, string expression)
            {
                this.ID = id;
                this.Index = index;
                this.Semantic = semantic;
                this.Expression = expression;
            }
        }
        
        public enum VariableDataType { Boolean, Integer, String };
        
        public class Variable
        {
            public string TechnicalName;
            public string DefaultValue;
            public VariableDataType DataType;
            public string Description;

          
            public Variable(string technicalName, string defaultValue, VariableDataType dataType)
            {
                this.TechnicalName = technicalName;
                this.DefaultValue = defaultValue;
                this.DataType = dataType;
                this.Description = string.Empty;
            }
            public Variable(string technicalName, string defaultValue, VariableDataType dataType, string description)
            {
                this.TechnicalName = technicalName;
                this.DefaultValue = defaultValue;
                this.DataType = dataType;
                this.Description = description;
            }
        }
        
        public class VariableSet
        {
            public string ID;
            public string TechnicalName;
            public List<Variable> Variables;
            
            public VariableSet(string id, string technicalName, List<Variable> variables)
            {
                this.ID = id;
                this.TechnicalName = technicalName;
                this.Variables = variables;
            }
        }
        
        public enum NodeType { FlowFragment, Dialogue, DialogueFragment, Hub, Jump, Connection, Condition, Instruction, Other };
        public class Node
        {
            public string id;
            public NodeType type;
            public List<Node> nodes;

            public Node()
            {
                id = string.Empty;
                type = NodeType.Other;
                nodes = new List<Node>();
            }

            public Node(string id, NodeType nodeType, List<Node> nodes)
            {
                this.id = id;
                this.type = nodeType;
                this.nodes = nodes;
            }
        }
        
        public class Hierarchy
        {
            public Node node;

            public Hierarchy()
            {
                node = null;
            }

            public Hierarchy(Node node)
            {
                this.node = node;
            }
        }
        
        public class Dialogue : Element
        {
            public List<Pin> Pins;
            public List<string> References;
            public bool IsDocument;
            

            public Dialogue(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position, List<Pin> pins, List<string> references, bool isDocument = false)
                : base(id, technicalName, displayName, text, features, position)
            {
                this.Pins = pins;
                this.References = references;
                this.IsDocument = isDocument;
            }
        }
        
        public class DialogueFragment : Element
        {
            public LocalizableText MenuText;
            public LocalizableText StageDirections;
            public string SpeakerIdRef;
            public List<Pin> Pins;

            public DialogueFragment()
            {
                MenuText = new LocalizableText();
                StageDirections = new LocalizableText();
                SpeakerIdRef = string.Empty;
                Pins = new List<Pin>();
            }

            public DialogueFragment(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position,
                LocalizableText menuText, LocalizableText stageDirections, string speakerIdRef, List<Pin> pins)
                : base(id, technicalName, displayName, text, features, position)
            {
                this.MenuText = menuText;
                this.StageDirections = stageDirections;
                this.SpeakerIdRef = speakerIdRef;
                this.Pins = pins;
            }
        }
        
        public class Hub : Element
        {
            public List<Pin> Pins;

            public Hub()
            {
                Pins = new List<Pin>();
            }

            public Hub(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position, List<Pin> pins)
                : base(id, technicalName, displayName, text, features, position)
            {
                this.Pins = pins;
            }
        }
        
        public class ConnectionRef
        {
            public string IDRef;
            public string PinRef;

            public ConnectionRef()
            {
                IDRef = string.Empty;
                PinRef = string.Empty;
            }

            public ConnectionRef(string idRef, string pinRef)
            {
                this.IDRef = idRef;
                this.PinRef = pinRef;
            }
        }
        
        public class Jump : Element
        {
            public ConnectionRef Target;
            public List<Pin> Pins;

            public Jump()
            {
                Target = new ConnectionRef();
                Pins = new List<Pin>();
            }

            public Jump(string id, string technicalName, LocalizableText displayName, LocalizableText text, Features features, Vector2 position, ConnectionRef target, List<Pin> pins)
                : base(id, technicalName, displayName, text, features, position)
            {
                this.Target = target;
                this.Pins = pins;
            }
        }
        
        public class Connection
        {
            public string ID;
            public string Color;
            public ConnectionRef Source;
            public ConnectionRef Target;
            
            public Connection(string id, string color, ConnectionRef source, ConnectionRef target)
            {
                this.ID = id;
                this.Color = color;
                this.Source = source;
                this.Target = target;
            }
        }
        
        public class Condition
        {
            public string ID;
            public string Expression;
            public List<Pin> Pins;
            public Vector2 Position;

            public Condition()
            {
                ID = string.Empty;
                Expression = string.Empty;
                Pins = new List<Pin>();
            }

            public Condition(string id, string expression, List<Pin> pins, Vector2 position)
            {
                this.ID = id;
                this.Expression = expression;
                this.Pins = pins;
                this.Position = position;
            }

            public Condition(string id, string expression, List<Pin> pins)
            {
                this.ID = id;
                this.Expression = expression;
                this.Pins = pins;
                this.Position = Vector2.zero;
            }
        }
        
        public class Instruction
        {
            public string ID;
            public string Expression;
            public List<Pin> Pins;
            public Vector2 Position;

            public Instruction()
            {
                ID = string.Empty;
                Expression = string.Empty;
                Pins = new List<Pin>();
            }

            public Instruction(string id, string expression, List<Pin> pins, Vector2 position)
            {
                this.ID = id;
                this.Expression = expression;
                this.Pins = pins;
                this.Position = position;
            }

            public Instruction(string id, string expression, List<Pin> pins)
            {
                this.ID = id;
                this.Expression = expression;
                this.Pins = pins;
                this.Position = Vector2.zero;
            }
        }


        public Project project = new Project();
        public Dictionary<string, Asset> assets = new Dictionary<string, Asset>();
        public Dictionary<string, Entity> entities = new Dictionary<string, Entity>();
        public Dictionary<string, Location> locations = new Dictionary<string, Location>();
        public Dictionary<string, FlowFragment> flowFragments = new Dictionary<string, FlowFragment>();
        public Dictionary<string, Dialogue> dialogues = new Dictionary<string, Dialogue>();
        public Dictionary<string, DialogueFragment> dialogueFragments = new Dictionary<string, DialogueFragment>();
        public Dictionary<string, Hub> hubs = new Dictionary<string, Hub>();
        public Dictionary<string, Jump> jumps = new Dictionary<string, Jump>();
        public Dictionary<string, Connection> connections = new Dictionary<string, Connection>();
        public Dictionary<string, Condition> conditions = new Dictionary<string, Condition>();
        public Dictionary<string, Instruction> instructions = new Dictionary<string, Instruction>();
        public Dictionary<string, VariableSet> variableSets = new Dictionary<string, VariableSet>();
        public List<string> textTableFields = new List<string>();
        public Hierarchy hierarchy = new Hierarchy();
        
        public string ProjectTitle { get { return project.DisplayName; } }
        public string ProjectVersion { get { return project.CreatedTime; } }
        public string ProjectAuthor { get { return string.Format("{0} {1}", project.CreatorTool, project.CreatorVersion); } }

        public static string FullVariableName(VariableSet variableSet, Variable variable)
        {
            return ((variableSet != null) && (variable != null))
                ? string.Format("{0}.{1}", variableSet.TechnicalName, variable.TechnicalName)
                : string.Empty;
        }

        public const string HighPriorityColor = "#FF0000";
        public const string AboveNormalPriorityColor = "#FFC000";
        public const string BelowNormalPriorityColor = "#FFFF00";
        public const string LowPriorityColor = "#92D050";

        public static ConditionPriority ColorToPriority(string color)
        {
            if (string.Equals(color, ArticyData.HighPriorityColor))
            {
                return ConditionPriority.High;
            }
            else if (string.Equals(color, ArticyData.AboveNormalPriorityColor))
            {
                return ConditionPriority.AboveNormal;
            }
            else if (string.Equals(color, ArticyData.BelowNormalPriorityColor))
            {
                return ConditionPriority.BelowNormal;
            }
            else if (string.Equals(color, ArticyData.LowPriorityColor))
            {
                return ConditionPriority.Low;
            }
            else
            {
                return ConditionPriority.Normal;
            }
        }
        
    }
}
