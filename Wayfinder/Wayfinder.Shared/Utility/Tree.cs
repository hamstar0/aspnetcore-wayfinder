﻿namespace Wayfinder.Shared.Utility.BooleanTree;



public class Tree<TreeData> {
    public IList<Tree<TreeData>> Children = new List<Tree<TreeData>>();

	public TreeData? Data;
}

