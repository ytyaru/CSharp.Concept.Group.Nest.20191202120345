using System;
using System.Collections.Generic;
using System.Linq;

namespace Concept.Linq.Lesson0 {
    class Human {
        public string Name { get; }
        public int Age { get; }
        public Human(string name, int age) => (Name, Age) = (name, age);
    }
    class Main {
        public void Run() {
            List<Human> humans = CreateHumans();
            Show(Query(humans));
        }
        private List<Human> CreateHumans() {
            return new List<Human> {
                new Human("A", 0), new Human("A", 1),
                new Human("B", 2), new Human("A", 3),
                new Human("B", 4), new Human("B", 5),
            };
        }
        private IEnumerable<IGrouping<string, IGrouping<bool, Human>>> Query(in List<Human> humans) {
            return from h in humans
                   group h by h.Name into g1
                   from g2 in (
                    from h in g1
                    group h by 0 == (h.Age % 2)
                   )
                   group g2 by g1.Key;
        }
        private void Show(in IEnumerable<IGrouping<string, IGrouping<bool, Human>>> groups) {
            foreach (var g1 in groups) {
                Console.WriteLine($"Key1={g1.Key}");
                foreach (var g2 in g1) {
                    Console.WriteLine($"  Key2=年齢が偶数か:{g2.Key}");
                    foreach (var v in g2) {
                        Console.WriteLine($"    Value=Name:{v.Name}, Age:{v.Age}");
                    }
                }
            }
        }
    }
}
