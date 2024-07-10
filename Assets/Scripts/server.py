import socket
from SGT import SGT
import PrepareClassifier
import libemg
import pickle
from libemg.utils import make_regex

HOST = 'localhost'  # Host IP address
PORT = 12345       # Port number to listen on


def StartServer():
    flag = True
    sgt=None
    try:
        # Create a TCP/IP socket
        server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        # Bind the socket to a specific address and port
        server_socket.bind((HOST, PORT))
        # Listen for incoming connections
        server_socket.listen(1)
        print(f"Server started on {HOST}:{PORT}")
        # Accept a client connection
        client_socket, client_address = server_socket.accept()
        print(f"Connection established with {client_address[0]}:{client_address[1]}")
        
        while flag:
            try:
                # Receive data from Unity
                data = client_socket.recv(1024).decode('utf-8')
                print(f"Received data: {data}")
                if data:
                    flag, sgt = ProcessReceivedData(client_socket, data, sgt, flag)
                    
                        
            except Exception as e:
                print(f"Error processing data: {e}")
        p.kill()
        odh.stop_listening()
        oc = PrepareClassifier.prepare_classifier(sgt.num_reps, sgt.input_count, sgt.output_folder)
        PrepareClassifier.start_live_classifier(oc)
    except Exception as e:
        print(f"Server error: {e}")
    finally:
        client_socket.close()
        server_socket.close()

def ProcessReceivedData(client_socket, data, sgt=None, flag=True):
    print("Received data from Unity:", data)
    if (data[0] == 'I'): #Initialization
        #SGT(data_handler, num_reps, time_per_reps, time_bet_rep, inputs_names, output_folder)
        split_idx = data.find(' ')
        sgt = SGT(odh, int(data[1]), int(data[2]), int(data[3]), data[4 : split_idx ], data[split_idx + 1:])
        return flag, sgt
    else:
        flag = sgt._collect_data(data[0])
        return flag, sgt
    


def SendData(client_socket, data):
    # Send response back to Unity client
    print(f"We made it")
    client_socket.send(data.encode('utf-8'))


if (__name__ == "__main__"):
    p = libemg.streamers.myo_streamer() #process to start myo giving out data
    odh = libemg.data_handler.OnlineDataHandler() #online data handler: process to start grabbing myo data
    odh.start_listening()
    StartServer()
