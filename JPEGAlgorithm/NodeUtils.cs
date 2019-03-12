using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class NodeUtils {
		public static List<Node> ToNodes(List<Tuple<int, int>> tuples) {
			return tuples.ConvertAll(t => new Node(0, t.Item1, t.Item2));
		}

		public static void showAsTree(Node tree) {
			StringBuilder sb = new StringBuilder();
			Dictionary<int, List<Node>> levels = new Dictionary<int, List<Node>>();
			IdCounter idCounter = new IdCounter(0);
			int level = 0;
			List<Node> nodes = new List<Node>();
			tree.id = idCounter.getId();
			nodes.Add(tree);
			levels.Add(level, nodes);
			while (nodes.Any()) {
				nodes = BuildLevel(levels[level], idCounter);
				levels.Add(++level, nodes);
			}
			for (int i = 0; i < level; i++) {
				sb.Append(string.Join<Node>(", ", levels[i].ToArray()));
				sb.Append("\n");
			}
			Console.WriteLine(sb.ToString());
		}

		private class IdCounter {
			private int id;

			public IdCounter(int id) {
				this.id = id;
			}

			public int getId() {
				return id;
			}

			public int increment() {
				return ++id;
			}
		}

		private static List<Node> BuildLevel(List<Node> nodes, IdCounter idCounter) {
			List<Node> res = new List<Node>();
			foreach (Node node in nodes) {
				node.id = idCounter.increment();
				if (node.left != null) {
					res.Add(node.left);
				}
				if (node.right != null) {
					res.Add(node.right);
				}
			}
			return res;
		}
	}
}
