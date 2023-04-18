from argparse import ArgumentParser
import os
import torch
import librosa
from gesticulator.model.model import GesticulatorModel
from gesticulator.interface.gesture_predictor import GesturePredictor
from gesticulator.visualization.motion_visualizer.generate_videos import Get_Motions_After_Creating_bvh

def main(args):
    # 0. Check feature type based on the model
    feature_type, audio_dim = check_feature_type(args.model_file)
    # 1. Load the model
    model = GesticulatorModel.load_from_checkpoint(args.model_file, inference_mode=True)
    # This interface is a wrapper around the model for predicting new gestures conveniently
    gp = GesturePredictor(model, feature_type)
    # 2. Predict the gestures with the loaded model
    motion = gp.predict_gestures(args.audio, args.text)
    # 3. get bvh of the motion
    motions = Get_Motions_After_Creating_bvh(motion_in=motion.detach(), bvh_file="temp.bvh", data_pipe_dir='../gesticulator/utils/data_pipe.sav')
    result = motions.tolist()
    return result

def Get_2DListInUnity(audioPath:str, text:str):
    args = parse_args()
    args.audio = audioPath
    args.text = text
    os.chdir("C:/Users/jongh/OneDrive/바탕 화면/Metaver_Project_120220121_Shinjonghyun/pythonGesticulator/demo")
    return main(args)


def check_feature_type(model_file):
    """
    Return the audio feature type and the corresponding dimensionality
    after inferring it from the given model file.
    """
    params = torch.load(model_file, map_location=torch.device('cpu'))

    # audio feature dim. + text feature dim.
    audio_plus_text_dim = params['state_dict']['encode_speech.0.weight'].shape[1]

    # This is a bit hacky, but we can rely on the fact that
    # BERT has 768-dimensional vectors
    # We add 5 extra features on top of that in both cases.
    text_dim = 768 + 5

    audio_dim = audio_plus_text_dim - text_dim

    if audio_dim == 4:
        feature_type = "Pros"
    elif audio_dim == 64:
        feature_type = "Spectro"
    elif audio_dim == 68:
        feature_type = "Spectro+Pros"
    elif audio_dim == 26:
        feature_type = "MFCC"
    elif audio_dim == 30:
        feature_type = "MFCC+Pros"
    else:
        print("Error: Unknown audio feature type of dimension", audio_dim)
        exit(-1)

    return feature_type, audio_dim


def truncate_audio(input_path, target_duration_sec):
    """
    Load the given audio file and truncate it to 'target_duration_sec' seconds.
    The truncated file is saved in the same folder as the input.
    """
    audio, sr = librosa.load(input_path, duration=int(target_duration_sec))
    output_path = input_path.replace('.wav', f'_{target_duration_sec}s.wav')

    librosa.output.write_wav(output_path, audio, sr)

    return output_path


def parse_args():
    parser = ArgumentParser()
    parser.add_argument('--audio', type=str, default="input/jeremy_howard.wav",
                        help="path to the input speech recording")
    parser.add_argument('--text', type=str, default="input/jeremy_howard.json",
                        help="one of the following: "
                             "1) path to a time-annotated JSON transcription (this is what the model was trained with) "
                             "2) path to a plaintext transcription, or "
                             "3) the text transcription itself (as a string)")
    parser.add_argument('--video_out', '-video', type=str, default="output/generated_motion.mp4",
                        help="the path where the generated video will be saved.")
    parser.add_argument('--model_file', '-model', type=str, default="models/default.ckpt",
                        help="path to a pretrained model checkpoint")
    parser.add_argument('--mean_pose_file', '-mean_pose', type=str, default="../gesticulator/utils/mean_pose.npy",
                        help="path to the mean pose in the dataset (saved as a .npy file)")

    return parser.parse_args()


if __name__ == "__main__":
    #args = parse_args()
    #main(args)
    audioPath = r"C:\Users\jongh\OneDrive\바탕 화면\Metaver_Project_120220121_Shinjonghyun\pythonGesticulator\demo\input\jeremy_howard.wav"
    text = "Deep learning is an algorithm inspired by how the human brain works, and as a result it's an algorithm which has no theoretical limitations on what it can do. The more data you give it and the more computation time you give it, the better it gets. The New York Times also showed in this article another extraordinary result of deep learning which I'm going to show you now. It shows that computers can listen and understand.";
    Get_2DListInUnity(audioPath, text)
