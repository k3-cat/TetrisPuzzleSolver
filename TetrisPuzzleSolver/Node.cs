namespace TetrisPuzzleSolver {
    class Node {
        public Node u;
        public Node d;
        public Node l;
        public Node r;
        public readonly Node col;
        public readonly int row;

        public Node(Node colNode, int row) {
            u = null;
            d = null;
            l = null;
            r = null;
            col = colNode;
            this.row = row;
        }
    }
}
