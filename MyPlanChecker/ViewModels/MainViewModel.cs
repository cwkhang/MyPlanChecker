using ESAPIX.Common;
using ESAPIX.Constraints.DVH;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VMS.TPS.Common.Model.API;

namespace MyPlanChecker.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {
            CreateConstraints();
            EvaluateCommand = new DelegateCommand(() =>
            {
                foreach (var pc in Constraints)
                {
                    var result = VMS.GetValue(scriptContext =>
                    {
                        //Check if we can constrain first
                        var canConstrain = pc.Constraint.CanConstrain(scriptContext.PlanSetup);
                        //If not..report why
                        if (!canConstrain.IsSuccess) { return canConstrain; }
                        else
                        {
                            //Can constrain - so do it
                            return pc.Constraint.Constrain(scriptContext.PlanSetup);
                        }
                    });
                    //Update UI
                    pc.Result = result;
                }
            });
        }

        private void CreateConstraints()
        {
            Constraints.AddRange(new PlanConstraint[]
            {
                new PlanConstraint(ConstraintBuilder.Build("PTV", "Max[%] <= 110")),
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V75Gy[%] <= 15")),
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V65Gy[%] <= 35")),
                new PlanConstraint(ConstraintBuilder.Build("Bladder", "V80Gy[%] <= 15")),
                new PlanConstraint(new CTDateConstraint())
            });
        }

        public DelegateCommand EvaluateCommand { get; set; }
        public ObservableCollection<PlanConstraint> Constraints { get; private set; } = new ObservableCollection<PlanConstraint>();
    }
}
