using System;
using System.Collections.Generic;
using KHPlayer.Classes;
using NAudio.CoreAudioApi;

namespace KHPlayer.Services
{
    public class AudioDeviceService
    {
        public List<AudioDevice> Get()
        {
            var result = new List<AudioDevice>();

            var enumerator = new MMDeviceEnumerator();
            for (var i = 0; i < enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).Count; i++)
            {
                var endpoint = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[i];
                result.Add(new AudioDevice
                {
                    Id = i,
                    Name = endpoint.FriendlyName
                });
            }

            return result;
        }
    }
}
