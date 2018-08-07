### Balancing Binary Tree

An implementation of a generic self-balancing binary tree targeting .Net Standard 2.

Balancing operations occur on insert, and bubble up through the tree.

The node searching operations defined in ```Tree.cs``` demonstrate how C#7 can be used in a functional style.

There are four balancing operations - two left-balancing, and their symmetrical right-balancing equivalents (not shown, since they are directly symmetrical.)

**Fig. 1.** below shows the first left balancing operation, occuring with node *A* as the 'current' node. It will occur when there are more left descendents of *A*'s left child *B*, than there are right descendents of *A*. 

The node *B*, and nodes to the left of *B* get one 'step' closer to the root context at the expense of node *A* and nodes to the right of *A*.

**Fig. 2.** shows the second left-balancing operation. Here, the operation balances when there are more right decendents of *B* than right descedents of *A*.

![rebalancing nodes diagram][nodes]

[nodes]: ../assets/balancing.jpg

### Notes

- The tree enumerates in ascending order and can be used to sort sequences of values. However, it's performance is less than that of a LINQ sort operation which uses a native implementation of a quick sort.
- Because the tree is balanced, it offers retrieval of the order O(log N).
- No delete-node operation is implemented at this time.
- Ordering is via the default Comparer< T > which will use IComparer< T > if your type T implements it.
- This means that you can customise sorting to, for example, order the tree from left to right in descending order.