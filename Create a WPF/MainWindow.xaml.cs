using System;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.IO;
using System.Collections.Generic;


namespace Create_a_WPF
{
    public partial class MainWindow : Window
    {
        private WorkflowDesigner wd;
        public MainWindow()
        {
            InitializeComponent();
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
            Grid.SetRow(wd.View, 1);
            // Load a new Sequence as default.
            wd.Load(new Sequence());
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
            Grid.SetRow(tc, 1);
            grid1.Children.Add(tc);
        }

        private void AddPropertyInspector()
        {
            Grid.SetColumn(wd.PropertyInspectorView, 2);
            Grid.SetRow(wd.PropertyInspectorView, 1);
            grid1.Children.Add(wd.PropertyInspectorView);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Tạo một StringWriter để thu thập kết quả
            var stringWriter = new StringWriter();
            var oldOut = Console.Out;
            // Chuyển hướng Console.Out đến StringWriter
            Console.SetOut(stringWriter);
            // Lưu Workflow từ WorkflowDesigner
            wd.Flush();
            string xamlText = wd.Text;
            // Nạp Workflow từ XAML
            Activity workflow = System.Activities.XamlIntegration.ActivityXamlServices.Load(new System.IO.StringReader(xamlText));
            // Chạy Workflow đồng bộ
            WorkflowInvoker.Invoke(workflow);
            // Hiển thị kết quả từ WriteLine
            string output = stringWriter.ToString();
            txtOutput.AppendText(DateTime.Now.ToString("dd/MM/yyyy") + "[Output]: " + output);
        }

      

    }
}
