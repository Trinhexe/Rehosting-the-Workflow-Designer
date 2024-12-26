using System.Windows;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.ComponentModel;
using System.Windows.Controls;
using System;
namespace Create_a_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkflowDesigner wd;


        public MainWindow()
        {
            InitializeComponent();
            // Register the metadata.
            RegisterMetadata();
            AddDesigner();
            AddToolBox();

            AddPropertyInspector();
        }
        private void AddDesigner()
        {
            // Create an instance of WorkflowDesigner class.
            wd = new WorkflowDesigner();

            // Place the designer canvas in the middle column of the grid.
            Grid.SetColumn(wd.View, 1);


            

            // Load a new Sequence as default.
            wd.Load(new Sequence());

            // Add the designer canvas to the grid.
            grid1.Children.Add(wd.View);
        }
        private void RegisterMetadata()
        {
            var dm = new DesignerMetadata();
            dm.Register();
        }
        private ToolboxControl GetToolboxControlFlow()
        {
            // Helper method to create ToolboxItemWrapper
            ToolboxItemWrapper CreateToolboxItem(string typeName, Type type, string displayName)
            {
                return new ToolboxItemWrapper(typeName, type.Assembly.FullName, null, displayName);
            }

            // Helper method to add items to a category
            void AddItemsToCategory(ToolboxCategory category, params ToolboxItemWrapper[] items)
            {
                foreach (var item in items)
                {
                    category.Add(item);
                }
            }

            // Create the ToolboxControl
            var ctrl = new ToolboxControl();

            // Create categories
            var controlFlowCategory = new ToolboxCategory("Control Flow");
            var flowChartCategory = new ToolboxCategory("Flow Chart");
            var stateMachineCategory = new ToolboxCategory("State Machine");
            var primitivesCategory = new ToolboxCategory("Primitives");
            var runtimeCategory = new ToolboxCategory("Runtime");
            var transactionCategory = new ToolboxCategory("Transaction");
            var errorCategory = new ToolboxCategory("Error Handling");
            

            // Create ToolboxItemWrapper instances
            var controlFlowItems = new[]
            {
                CreateToolboxItem("System.Activities.Statements.If", typeof(If), "If"),
                CreateToolboxItem("System.Activities.Statements.Pick", typeof(Pick), "Pick"),
                CreateToolboxItem("System.Activities.Statements.DoWhile", typeof(DoWhile), "DoWhile"),
                CreateToolboxItem("System.Activities.Statements.Parallel", typeof(Parallel), "Parallel"),
                CreateToolboxItem("System.Activities.Statements.Sequence", typeof(Sequence), "Sequence"),
                CreateToolboxItem("System.Activities.Statements.PickBranch", typeof(PickBranch), "PickBranch"),
                CreateToolboxItem("System.Activities.Statements.While", typeof(While), "While")
            };

            var flowChartItems = new[]
            {
                CreateToolboxItem("System.Activities.Statements.FlowChart", typeof(Flowchart), "FlowChart"),
                CreateToolboxItem("System.Activities.Statements.FlowDecision", typeof(FlowDecision), "FlowDecision")
            };

            var stateMachineItems = new[]
            {
                CreateToolboxItem("System.Activities.Statements.StateMachine", typeof(StateMachine), "StateMachine"),
                CreateToolboxItem("System.Activities.Statements.State", typeof(State), "State")
            };

            var primitivesItems = new[]
            {
                 CreateToolboxItem("System.Activities.Statements.Assign", typeof(Assign), "Assign"),
                 CreateToolboxItem("System.Activities.Statements.Delay", typeof(Delay), "Delay"),
                 CreateToolboxItem("System.Activities.Statements.InvokeDelegate", typeof(InvokeDelegate), "InvokeDelegate"),
                 CreateToolboxItem("System.Activities.Statements.InvokeMethod", typeof(InvokeMethod), "InvokeMethod"),
                 CreateToolboxItem("System.Activities.Statements.WriteLine", typeof(WriteLine), "WriteLine"),
            };

            var runtimeItems = new[]
            {
                CreateToolboxItem("System.Activities.Statements.Persist", typeof(Assign), "Persist"),
                CreateToolboxItem("System.Activities.Statements.TerminateWorkflow", typeof(TerminateWorkflow), "TerminateWorkflow"),
                CreateToolboxItem("System.Activities.Statements.NoPersistScope", typeof(NoPersistScope), "NoPersistScope"),
            };

            var transactionItems = new[]
            {
                CreateToolboxItem("System.Activities.Statements.CancellationScope", typeof(CancellationScope), "CancellationScope"),
                CreateToolboxItem("System.Activities.Statements.CompensableActivity", typeof(CompensableActivity), "CompensableActivity"),
                CreateToolboxItem("System.Activities.Statements.Compensate", typeof(Compensate), "Compensate"),
                CreateToolboxItem("System.Activities.Statements.Confirm", typeof(Confirm), "Confirm"),
                CreateToolboxItem("System.Activities.Statements.TransactionScope", typeof(TransactionScope), "TransactionScope"),
            };

            var errorItems = new[]
            {
                CreateToolboxItem("System.Activities.Statements.Rethrow", typeof(Rethrow), "Rethrow"),
                CreateToolboxItem("System.Activities.Statements.Throw", typeof(Throw), "Throw"),
                CreateToolboxItem("System.Activities.Statements.TryCatch", typeof(TryCatch), "TryCatch"),
            };
            // Add items to categories
            AddItemsToCategory(controlFlowCategory, controlFlowItems);
            AddItemsToCategory(flowChartCategory, flowChartItems);
            AddItemsToCategory(stateMachineCategory, stateMachineItems);
            AddItemsToCategory(primitivesCategory, primitivesItems);
            AddItemsToCategory(runtimeCategory, runtimeItems);
            AddItemsToCategory(transactionCategory,transactionItems);
            AddItemsToCategory(errorCategory,errorItems);
           

            // Add categories to the ToolboxControl
            ctrl.Categories.Add(controlFlowCategory);
            ctrl.Categories.Add(flowChartCategory);
            ctrl.Categories.Add(stateMachineCategory);
            ctrl.Categories.Add(primitivesCategory);
            ctrl.Categories.Add(runtimeCategory);
            ctrl.Categories.Add(transactionCategory);
            ctrl.Categories.Add(errorCategory);
            return ctrl;
        }

        private void AddToolBox()
        {
            ToolboxControl tc = GetToolboxControlFlow();
            Grid.SetColumn(tc, 0);
            grid1.Children.Add(tc);
        }

        private void AddPropertyInspector()
        {
            Grid.SetColumn(wd.PropertyInspectorView, 2);
            grid1.Children.Add(wd.PropertyInspectorView);
        }

    }
}
