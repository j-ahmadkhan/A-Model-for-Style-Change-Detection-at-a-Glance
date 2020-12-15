# A-Model-for-Style-Change-Detection-at-a-Glance

PAN 2018 Author Identification sub-task for style change detection deals with a single question, whether or not a document has multiple authors? 
To answer this simple question, a simple straightforward and fast approach is proposed in this document. Some basic stylometry analysis
techniques e.g. word frequencies (for stop-words and other POS words), punctuations, word pair frequencies and POS pair frequencies. In order to make
fast comparison among word windows, a fast comparison model is built that can produce results in a glance. This model showed 65.1% accuracy over
evaluation dataset and 63.83% accuracy over training dataset. 

Software here is written in c# and anyone can test it by placing "English" Language documents into 'PAN' folder and results are generated into 'Results' folder.
or you can set the input directory via -i option and output directory via -o option.
A complete description of the Algorithm used can be found at http://ceur-ws.org/Vol-2125/paper_170.pdf
