using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    [Challenge(Challenge.Done)]
    class CourseMaterials : IChallenge<List<CourseEntry>, List<string>> {
        public List<string> ExpectedOutput() {
            return new List<string> {
                "US History",
                "-First Chapter",
                "--Third Assignment",
                "-Second Assignment",
                "Canadian History",
                "-First Assignment"
            };
        }

        public List<CourseEntry> Input() {
            return new List<CourseEntry>() {
                new CourseEntry(1, "US History", -1),
                new CourseEntry(2, "Third Assignment", 3),
                new CourseEntry(3, "First Chapter", 1),
                new CourseEntry(4, "Second Assignment", 1),
                new CourseEntry(5, "First Assignment", 6),
                new CourseEntry(6, "Canadian History", -1),
            };
        }

        public List<string> Run(List<CourseEntry> input) {

            var orphans = new Dictionary<int, List<CourseNode>>();
            var seenNodes = new Dictionary<int, CourseNode>();
            var roots = new Dictionary<int, CourseNode>();
            var toReturn = new List<string>();

            foreach (var course in input) {
                var parent = course.ParentID;
                var courseNode = new CourseNode(course.ID, course.Name);

                seenNodes.Add(courseNode.ID, courseNode);

                // if the node is a root
                if (parent == -1) {
                    roots.Add(courseNode.ID, courseNode);
                }

                // the node is not a root, find its parent
                // first, check if we have seen its parent
                if (seenNodes.ContainsKey(parent)) {
                    // add the node to this parent
                    seenNodes[parent].Children.Add(courseNode);
                } else {
                    // it is an orphan, add it to the orphan list
                    if (!orphans.ContainsKey(parent)) {
                        orphans.Add(parent, new List<CourseNode>());
                    }
                    orphans[parent].Add(courseNode);
                }

                // now this node need to collect the orphans
                if (orphans.ContainsKey(courseNode.ID)) {
                    var children = orphans[courseNode.ID];
                    foreach (var child in children) {
                        courseNode.Children.Add(child);
                    }
                    orphans.Remove(courseNode.ID);
                }
            }

            foreach (var root in roots.Values) {
                PrintChildren(root, "", toReturn);
            }

            return toReturn;
        }

        public void PrintChildren(CourseNode node, string prefix, List<string> toReturn) {
            toReturn.Add($"{prefix}{node.Name}");
            if (node.Children.Count > 0) {
                foreach (var child in node.Children) {
                    PrintChildren(child, prefix + "-", toReturn);
                }
            }
        }
    }

    class CourseEntry {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int ParentID { get; private set; }

        public CourseEntry(int id, string name, int parentID) {
            ID = id;
            Name = name;
            ParentID = parentID;
        }
    }

    class CourseNode {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<CourseNode> Children { get; private set; }

        public CourseNode(int id, string name) {
            ID = id;
            Name = name;
            Children = new List<CourseNode>();
        }
    }
}
