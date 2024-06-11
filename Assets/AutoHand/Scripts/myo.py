from libemg.streamers import myo_streamer
from libemg.data_handler import OnlineDataHandler, OfflineDataHandler
from libemg.screen_guided_training import ScreenGuidedTraining
from libemg.utils import *
from libemg.emg_classifier import EMGClassifier, OnlineEMGClassifier
from libemg.feature_extractor import FeatureExtractor

WINDOW_SIZE = 40
WINDOW_INCREMENT = 20
feature_set = ['WENG']
feature_dic = {'WENG_fs': 200}

if __name__ == "__main__":
    myo_streamer()
    online_dh = OnlineDataHandler()
    online_dh.start_listening()

    # Screen Guided Training
    # sgt = ScreenGuidedTraining()
    # sgt.download_gestures([1,2,3,4, 5], 'images/')
    # sgt.launch_training(online_dh, num_reps=3, rep_time=3, rep_folder='images/', output_folder='data/', time_between_reps=1)

    # Load data and create classifier
    dataset_folder = 'data/'
    classes_values = ["0","1","2","3","4"]
    classes_regex = make_regex(left_bound = "_C_", right_bound="_EMG", values = classes_values)
    reps_values = ["0", "1", "2"]
    reps_regex = make_regex(left_bound = "R_", right_bound="_C_", values = reps_values)
    dic = {
        "reps": reps_values,
        "reps_regex": reps_regex,
        "classes": classes_values,
        "classes_regex": classes_regex
    }

    odh = OfflineDataHandler()
    odh.get_data(folder_location=dataset_folder, filename_dic=dic, delimiter=",")
    train_windows, train_metadata = odh.parse_windows(WINDOW_SIZE, WINDOW_INCREMENT)

    fe = FeatureExtractor()
    train_feats = fe.extract_features(feature_set, train_windows, feature_dic)

    data_set = {}
    data_set['training_features'] = train_feats
    data_set['training_labels'] = train_metadata['classes']

    o_classifier = EMGClassifier()
    o_classifier.fit(model="LDA", feature_dictionary=data_set)
    o_classifier.install_feature_parameters(feature_dic)

    classifier = OnlineEMGClassifier(o_classifier, WINDOW_SIZE, WINDOW_INCREMENT, online_dh, feature_set, std_out=True, tcp=True, port=8099, ip='192.168.137.168')
    classifier.run(block=True)

