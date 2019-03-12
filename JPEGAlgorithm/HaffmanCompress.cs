using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class HaffmanCompress {
		private IEnumerable<Tuple<int, int>> tuples;

		private Node tree;

		private Dictionary<int, string> codeTable;

		public HaffmanCompress(IEnumerable<Tuple<int, int>> tuples) {
			this.tuples = tuples;
		}

		public Dictionary<int, string> CodeTable { get => codeTable; }
		public Node Tree { get => tree; }

		private class NodeComparator : IComparer<Node> {
			public int Compare(Node x, Node y) {
				return -x.weight.CompareTo(y.weight);
			}
		}

		public void buildTree() {
			SortedSet<Node> nodes = new SortedSet<Node>(NodeUtils.ToNodes(tuples.ToList()), new NodeComparator());
			Node last;
			Node preLast;
			Node temp;
			while (nodes.Count != 1) {
				last = nodes.Last();
				nodes.Remove(last);
				preLast = nodes.Last();
				nodes.Remove(preLast);
				temp = new Node(last.weight + preLast.weight);
				temp.left = preLast;
				temp.right = last;
				preLast.parent = temp;
				last.parent = temp;
				nodes.Add(temp);
			}
			tree = nodes.First();
		}

		public void buildCodeTable() {
			if (codeTable == null) {
				Node root = this.tree;
				codeTable = new Dictionary<int, string>();
				StringBuilder code = new StringBuilder();
				Visit(root, code);
			}
		}

		private void Visit(Node node, StringBuilder code) {
			node.isVisited = true;
			if (node.left != null) {
				code.Append("0");
				Visit(node.left, code);
			}
			if (node.right != null) {
				code.Append("1");
				Visit(node.right, code);
			}
			if (node.left == null && node.right == null) {
				codeTable.Add((int)node.value, code.ToString());
				code.Remove(code.Length - 1, 1);
			} else {
				if (node.left.isVisited && node.right.isVisited && code.Length > 0) {
					code.Remove(code.Length - 1, 1);
				}
			}
			/*if (node.parent != null && node.parent.right != null && node.parent.right == node) {
				code.Remove(0, 1);
			}*/
		}
	}
}
