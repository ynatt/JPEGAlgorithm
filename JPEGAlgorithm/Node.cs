using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm{
    class Node{
        public int id;
        public Node parent;
        public Node left;
        public Node right;
        public int? value;
        public int weight;
		public bool isVisited;

        public Node(int weight) {
			this.weight = weight;
        }

		public Node(int id, int? value, int weight) {
			this.id = id;
			this.value = value;
			this.weight = weight;
		}

		
		public override string ToString() {
			return string.Format("[id = {0}, pId = {1}, v = {2}, w = {3}]", id, parent != null ? parent.id.ToString() : "null", value, weight);
		}
	}
}
