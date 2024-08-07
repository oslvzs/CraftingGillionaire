using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CraftingGillionaire.ViewModels;
using System;

namespace CraftingGillionaire
{
	public class ViewLocator : IDataTemplate
	{
		public Control Build(object? data)
		{
			if(data is null)
				throw new ArgumentNullException(nameof(data));

            string name = data.GetType().FullName!.Replace("ViewModel", "View");
            Type? type = Type.GetType(name);

			if (type != null)
			{
				return (Control)Activator.CreateInstance(type)!;
			}

			return new TextBlock { Text = "Not Found: " + name };
		}

		public bool Match(object? data)
		{
			if (data is null)
				throw new ArgumentNullException(nameof(data));

			return data is ViewModelBase;
		}
	}
}