# HeatmapProcessingApplication
Server-client application where server creates heat-map for photo and then return back to client

Two applications (client and server) that communicate with each other via TCP / IP protocol.
Both applications run on the same computer via the address: http://127.0.0.1. There are communicating using a TCP socket.

The application client opens a TCP socket and sends a string (select the string itself) informing the server application of its existence.
After a positive response from the server application, the application client sends the image and csv file that are specified (Heineken.jpg and Heineken.csv).

The application server processes the image using information from the csv file. 
The csv file contains time information and the corresponding coordinates of the image where at that point in time the view of the person viewing the given image was. 
The csv file is in the format: "timestamp; x_coordinate; y_coordinate".

User view location information should be mapped to a given input image to obtain a heatmap (which should show the density of the user view layout, 
and an example is given as Heineken_heatMap.jpg) - the color choice for the heatmap is arbitrary.

The image with heatmap is displayed in the original aspect ratio in UI.
