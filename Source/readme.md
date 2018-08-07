# Balancing Binary Tree

Implements a self-balancing binary tree. Balancing operations occur on insert, and bubble up through the tree.

The node searching operations defined in ```Tree.cs``` demonstrate how C#7 can be used in a functional style.

There are four balancing operations, two for left balancing and their symmetrical right-balancing equivalents.

**Fig. 1.** below defines the first left balancing operation, occuring with node *A* as the 'current' node. It will occur when there are more left descendents of *A*'s left child *B*, than there are right descendents of *A*. 

The node *B*, and nodes to the left of *B* get one 'step' closer to the root context at the expense of node *A* and nodes to the right of *A*.

![rebalancing nodes diagram][nodes]

[nodes]: balancing.jpg