using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Utilizing a dictionary and doubly linked list intially is not really a good strategy as it can introduce the possibily of loops or fragments which makes it more complicated
    /// to piece together. Basically this became a multistep problem because you have to 
    /// 1. sort the entries by destination
    /// 2. construct the hash map
    /// 3. link individual departure -> destination pairs
    /// 4. ensure that a single link will not cause loop overall
    /// 5. piece together the fragments
    /// Therefore, this strategy is not fesible to create a good solution
    /// 
    /// What is interesting from this challenge though is that what is created is essentially an Eulerian path, meaning that every edge is visited once
    /// 
    /// </summary>
    [Challenge(Challenge.DoNotRun)]
    class ReconstructItinerary : IChallenge<string[,], IList<string>> {
        public IList<string> ExpectedOutput() {
            var iterList = new List<string>(new string[] { "JFK", "ATL", "JFK", "SFO", "ATL", "SFO" });
            return iterList;
        }

        public string[,] Input() {
            var ticketList = new string[5, 2] { { "JFK", "SFO" }, { "JFK", "ATL" }, { "SFO", "ATL" }, { "ATL", "JFK" }, { "ATL", "SFO" } };
            return ticketList;
        }

        public IList<string> Run(string[,] input) {
            var tickets = input;

            var flightList = new List<FlightPair>();
            for (int i = 0; i < input.GetLength(0); i++) {
                flightList.Add(new FlightPair(input[i, 0], input[i, 1]));
            }
            flightList.Sort();

            var airportMap = new Dictionary<string, FlightMap>();

            foreach (var pair in flightList) {
                if (!airportMap.ContainsKey(pair.Departure)) {
                    airportMap.Add(pair.Departure, new FlightMap(pair.Departure));
                }

                if (!airportMap.ContainsKey(pair.Destination)) {
                    airportMap.Add(pair.Destination, new FlightMap(pair.Destination));
                }

                var departures = airportMap[pair.Departure].GetNoNextNodes();
                if (departures.Count == 0) {
                    var departure = new FlightNode(pair.Departure);
                    airportMap[pair.Departure].AddNode(departure);
                    departures.Add(departure);
                }
                var destinations = airportMap[pair.Destination].GetNoPrevNodes();
                if (destinations.Count == 0) {
                    var destination = new FlightNode(pair.Destination);
                    airportMap[pair.Destination].AddNode(destination);
                    destinations.Add(destination);
                }

                var valid = false;

                foreach (var departure in departures) {
                    if (valid) {
                        continue;
                    }
                    foreach (var destination in destinations) {
                        if (valid) {
                            continue;
                        }
                        departure.Next = destination;
                        destination.Prev = departure;
                        var hasLoop = TestForLoop(departure);
                        if (hasLoop) {
                            departure.Next = null;
                            destination.Prev = null;
                        } else {
                            valid = true;
                        }
                    }
                }
            }

            var sampleNode = airportMap[input[0, 0]].Nodes[0];
            while (sampleNode.HasPrev()) {
                sampleNode = sampleNode.Prev;
            }

            var resultList = new List<string>();
            while (sampleNode != null) {
                resultList.Add(sampleNode.Name);
                sampleNode = sampleNode.Next;
            }

            return resultList;
        }

        private bool TestForLoop(FlightNode node) {
            var slowPointer = node;
            var fastPointer = node;
            while (fastPointer != null) {
                slowPointer = slowPointer.Next;
                fastPointer = fastPointer.Next;
                if (fastPointer == null) {
                    return false;
                }
                fastPointer = fastPointer.Next;
                if (fastPointer == slowPointer) {
                    return true;
                }
            }
            return false;
        }
    }

    class FlightMap {
        public string Name { get; }
        public List<FlightNode> Nodes { get; }

        public FlightMap(string name) {
            Name = name;
            Nodes = new List<FlightNode>();
        }

        public void AddNode(FlightNode node) {
            if (node.Name != Name) {
                throw new ArgumentException($"Given node {node.Name} is not the same as map node {Name}");
            }
            Nodes.Add(node);
        }

        public List<FlightNode> GetNoPrevNodes() {
            var noPrevNodes = new List<FlightNode>();
            foreach (var node in Nodes) {
                if (!node.HasPrev()) {
                    noPrevNodes.Add(node);
                }
            }
            return noPrevNodes;
        }

        public List<FlightNode> GetNoNextNodes() {
            var noNextNodes = new List<FlightNode>();
            foreach (var node in Nodes) {
                if (!node.HasNext()) {
                    noNextNodes.Add(node);
                }
            }
            return noNextNodes;
        }
    }

    class FlightNode {

        public string Name { get; }
        public FlightNode Next { get; set; }
        public FlightNode Prev { get; set; }

        public FlightNode(string name) {
            Name = name;
        }

        public bool HasNext() {
            return Next != null;
        }

        public bool HasPrev() {
            return Prev != null;
        }
    }

    class FlightPair : IComparable {
        public string Departure { get; }
        public string Destination { get; }

        public FlightPair(string departure, string destination) {
            Departure = departure;
            Destination = destination;
        }

        public int CompareTo(object obj) {
            var o = obj as FlightPair;

            var departCompare = Departure.CompareTo(o.Departure);
            if (departCompare == 0) {
                return Destination.CompareTo(o.Destination);
            }

            return departCompare;
        }
    }
}
