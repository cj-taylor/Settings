﻿
using System.Collections.Generic;
using Settings.Common.Domain;
using Settings.Common.Models;

namespace Settings.Services
{
    //TODO: The plan here is to create a tree view, but EF might already give me that?
    //I know if i pull an entity this will hit n+1 issue, but if I preload all items
    //I think I can avoid subsequent hits to database.  Need to test that theory
    public class HierarchyHelper
    {

        //TODO: Since I only intend on environments and applications to be hierarchical,
        // would it be less messy to just write two distinct methods.  I like the reusability 
        //of the method below using IHierarchicalMold<T> but seems kinda confusing


        //public HierarchyItem GetEnvironmentTree(Environment node, HierarchyItem parentNode)
        //{
        //    var rootNode = new HierarchyItem()
        //    {
        //        LeftWeight = node.LeftWeight,
        //        RightWeight = node.RightWeight,
        //        Parent = parentNode,
        //        Children = new List<HierarchyItem>()
        //    };

        //    foreach (var child in node.Children)
        //    {
        //        rootNode.Children.Add(GetEnvironmentTree(child, rootNode));
        //    }

        //    return rootNode;
        //}

        //public HierarchyItem GetEnvironmentTree(Environment node)
        //{
        //    return GetEnvironmentTree(node, null);
        //}




        //TODO: Good candidate for unit testing!
        public HierarchicalModel GetHierarchicalTree<T>(IHierarchicalItem<T> node) 
            where T : IHierarchicalItem<T>
        {
            var rootNode = new HierarchicalModel()
            {
                Id = node.Id,
                LeftWeight = node.LeftWeight,
                RightWeight = node.RightWeight,
                Name = node.Name,
                Children = new List<HierarchicalModel>()
            };

            foreach (var child in node.Children)
            {
                rootNode.Children.Add(GetHierarchicalTree<T>(child));
            }

            return rootNode;
        }
    }
}