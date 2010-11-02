using System;
using System.Windows.Input;
using Castle.Core;
using Castle.Samples.WindsorSilverlight.Security;

namespace Castle.Samples.WindsorSilverlight.Commands
{
	[CastleComponent(typeof(DeleteAllCommand), Lifestyle = LifestyleType.Transient)]
	public class DeleteAllCommand : ICommand
	{
		public DeleteAllCommand()
		{
			
		}

		#region ICommand Members

		public virtual bool CanExecute(object parameter)
		{
			return true;
		}

		[Authorize(Role = "Admin")]
		public virtual void Execute(object parameter)
		{
			//administrative level entry only.
			//do your crazy stuff in here.
		}

		public event EventHandler CanExecuteChanged;

		#endregion
	}
}