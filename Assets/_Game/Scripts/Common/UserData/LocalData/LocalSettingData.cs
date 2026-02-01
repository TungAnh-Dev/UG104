namespace CasualGameArchitecture.Scripts.UserData
{
    using System;

    public class LocalSettingData : ILocalData
    {
        public float Sensitivity;
        public float MusicVolume = 1f;
        public float SoundVolume = 1f;
        public bool  IsVibrationEnabled;

        [NonSerialized] public Action<float> OnMusicVolumeChanged;
        [NonSerialized] public Action<float> OnSoundVolumeChanged;
        [NonSerialized] public Action<bool>  OnVibrationChanged;

        public void Init()
        {
            this.Sensitivity        = 0.5f;
            this.IsVibrationEnabled = true;
        }

        public void OnDataLoaded() { }

        public void SetMusicVolume(float value)
        {
            this.MusicVolume = value;
            this.OnMusicVolumeChanged?.Invoke(value);
        }

        public void SetSoundVolume(float value)
        {
            this.SoundVolume = value;
            this.OnSoundVolumeChanged?.Invoke(value);
        }

        public void ToggleVibration()
        {
            this.IsVibrationEnabled = !this.IsVibrationEnabled;
            this.OnVibrationChanged?.Invoke(this.IsVibrationEnabled);
        }
    }
}