package md5fde8c6fc4a8c39be3423a3807afda47d;


public class RecyclerViewScrollListener
	extends android.support.v7.widget.RecyclerView.OnScrollListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollStateChanged:(Landroid/support/v7/widget/RecyclerView;I)V:GetOnScrollStateChanged_Landroid_support_v7_widget_RecyclerView_IHandler\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Autocomplete.RecyclerViewScrollListener, Syncfusion.SfAutoComplete.Android", RecyclerViewScrollListener.class, __md_methods);
	}


	public RecyclerViewScrollListener ()
	{
		super ();
		if (getClass () == RecyclerViewScrollListener.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Autocomplete.RecyclerViewScrollListener, Syncfusion.SfAutoComplete.Android", "", this, new java.lang.Object[] {  });
	}


	public void onScrollStateChanged (android.support.v7.widget.RecyclerView p0, int p1)
	{
		n_onScrollStateChanged (p0, p1);
	}

	private native void n_onScrollStateChanged (android.support.v7.widget.RecyclerView p0, int p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
