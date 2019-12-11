using System.Collections;
using System.Linq;

namespace TetrisPuzzleSolver {
    class DancingLinks {
        readonly Stack ans;
        readonly Node head;

        public DancingLinks(Matrix m) {
            ans = new Stack();
            var row0 = new Node[m.totalCol + 1];
            for (var i = 0; i < row0.Length; i++) {
                row0[i] = new Node(null, 0);
            }
            head = row0[0];
            for (var i = 0; i < row0.Length; i++) {
                var colNode = row0[i];
                colNode.l = row0[(i - 1 + row0.Length) % row0.Length];
                colNode.r = row0[(i + 1 + row0.Length) % row0.Length];
                colNode.u = colNode;
                colNode.d = colNode;
            }
            for (var i = 0; i < m.fakeMatrix.Count; i++) {
                var rowChain = new Node[m.fakeMatrix[i].Length];
                for (var j = 0; j < rowChain.Length; j++) {
                    rowChain[j] = new Node(row0[m.fakeMatrix[i][j]], i + 1);
                }
                for (var j = 0; j < rowChain.Length; j++) {
                    var node = rowChain[j];
                    node.l = rowChain[(j - 1 + rowChain.Length) % rowChain.Length];
                    node.r = rowChain[(j + 1 + rowChain.Length) % rowChain.Length];

                    node.d = node.col;
                    node.u = node.col.u;
                    node.col.u.d = node;
                    node.col.u = node;
                }
            }
        }

        public bool Solve() {
            Node colNode = head.l;
            Node colNode_;
            Node rowNode;
            Node rowNode_;
            Node node;
            Node node_;

            if (colNode == head) {
                return true;
            }
            colNode.r.l = colNode.l;
            colNode.l.r = colNode.r;
            for (rowNode = colNode.d; rowNode != colNode; rowNode = rowNode.d) {
                for (node = rowNode.l; node != rowNode; node = node.l) {
                    node.u.d = node.d;
                    node.d.u = node.u;
                }
            }
            for (rowNode = colNode.d; rowNode != colNode; rowNode = rowNode.d) {
                ans.Push(rowNode.row);
                for (node = rowNode.l; node != rowNode; node = node.l) {
                    colNode_ = node.col;
                    colNode_.r.l = colNode_.l;
                    colNode_.l.r = colNode_.r;
                    for (rowNode_ = colNode_.d; rowNode_ != colNode_; rowNode_ = rowNode_.d) {
                        for (node_ = rowNode_.l; node_ != rowNode_; node_ = node_.l) {
                            node_.u.d = node_.d;
                            node_.d.u = node_.u;
                        }
                    }
                }
                if (Solve()) {
                    return true;
                }
                for (node = node.r; node != rowNode; node = node.r) {
                    colNode_ = node.col;
                    colNode_.r.l = colNode_;
                    colNode_.l.r = colNode_;
                    for (rowNode_ = colNode_.u; rowNode_ != colNode_; rowNode_ = rowNode_.u) {
                        for (node_ = rowNode_.r; node_ != rowNode_; node_ = node_.r) {
                            node_.u.d = node_;
                            node_.d.u = node_;
                        }
                    }
                }
                ans.Pop();
            }
            colNode.r.l = colNode;
            colNode.l.r = colNode;
            for (rowNode = rowNode.u; rowNode != colNode; rowNode = rowNode.u) {
                for (node = rowNode.r; node != rowNode; node = node.r) {
                    node.u.d = node;
                    node.d.u = node;
                }
            }
            return false;
        }

        public int[] GetAns() {
            return ans.ToArray().Cast<int>().ToArray();
        }
    }
}
