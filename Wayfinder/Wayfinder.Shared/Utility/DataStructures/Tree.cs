namespace Wayfinder.Shared.Utility.DataStructures;



public class Tree<TreeData> {
	public IList<Tree<TreeData>> Children = new List<Tree<TreeData>>();

	public TreeData? Data;
}

