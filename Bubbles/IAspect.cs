using System;
using ZiZZi;

namespace Existence
{
	public interface IAspect
	{
		string Name();
		IBlox Vault();
        TFormat As<TFormat>(IMatter<TFormat> matter) where TFormat : class;
        TBlueprint Into<TBlueprint>(TBlueprint blueprint) where TBlueprint : class;
	}
}

