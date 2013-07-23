using System;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Components {

    public class Word {
        int occurrence = 0;
        int weight = 0;
        string word;

        public Word(string word, WordLocation location) {
            this.word = word;
            IncrementOccurence(location);
        }

        public void IncrementOccurence(WordLocation location) {

            if (location == WordLocation.Forum)
                weight += 5;
            if (location == WordLocation.Subject)
                weight += 10;
            else 
                occurrence++;

        }

        public string Name {
            get {
                return word;
            }
        }

        public int Weight {
            get {
                return weight;
            }
        }

        public int Occurence {
            get {
                return occurrence;
            }
        }

    }


}
