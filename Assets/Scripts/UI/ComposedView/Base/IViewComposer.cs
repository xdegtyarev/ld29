using System;

public interface IViewComposer
{
	Type GetDataType();
	void CreateView(object c);
	void Hide();
}

