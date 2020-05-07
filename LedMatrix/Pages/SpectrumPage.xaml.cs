using LedMatrix.Context;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.Render;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LedMatrix.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SpectrumPage : Page
	{
		private AudioGraph _graph;
		private AudioDeviceInputNode _inputNode;
		private AudioFrameOutputNode _frameOutputNode;
		private float currentPeak;
		private DispatcherTimer timer;

		public SpectrumPage()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			_graph?.Stop();
			_graph?.Dispose();
			_graph = null;
		}

		private void BtnSpectrum_Click(object sender, RoutedEventArgs e)
		{
			CreateAudioGraphAsync();

			timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(1)
			};
			timer.Tick += (s, o) =>
			{
				try
				{
					ProgressBarVolume.Value = currentPeak * 100;





					//foreach (Pixel pixel in Util.Context.Pixels)
					//{
					//	if ((19 - pixel.Coord.Y) < currentPeak * 20)
					//	{
					//		pixel.SetColor(new Color { B = (byte)(5 + pixel.Coord.Y * 5) });

					//		//Debug.WriteLine(Math.Round(max * 20, 0).ToString() + " - " + pixel.Coord.Y);

					//	}
					//	else
					//		pixel.SetColor(new Color());
					//}




					//Util.SetLeds();


				}
				catch
				{

				}
			};

			timer.Start();
		}

		private async void CreateAudioGraphAsync()
		{

			AudioGraphSettings settings = new AudioGraphSettings(AudioRenderCategory.Media)
			{
				//settings.DesiredSamplesPerQuantum = fftLength;
				DesiredRenderDeviceAudioProcessing = AudioProcessing.Default,
				QuantumSizeSelectionMode = QuantumSizeSelectionMode.ClosestToDesired
			};

			CreateAudioGraphResult graphResult = await AudioGraph.CreateAsync(settings);

			if (graphResult.Status != AudioGraphCreationStatus.Success)
				throw new InvalidOperationException($"Graph creation failed {graphResult.Status}");

			_graph = graphResult.Graph;

			//CreateAudioDeviceInputNodeResult inputNodeResult = await _graph.CreateDeviceInputNodeAsync(MediaCategory.Media);
			CreateAudioDeviceInputNodeResult inputNodeResult = await _graph.CreateDeviceInputNodeAsync(MediaCategory.Other);

			if (inputNodeResult.Status == AudioDeviceNodeCreationStatus.Success)
			{

				_inputNode = inputNodeResult.DeviceInputNode;


				_frameOutputNode = _graph.CreateFrameOutputNode();
				_inputNode.AddOutgoingConnection(_frameOutputNode);
				_frameOutputNode.Start();
				_graph.QuantumProcessed += AudioGraph_QuantumProcessed;

				// Because we are using lowest latency setting, we need to handle device disconnection errors
				_graph.UnrecoverableErrorOccurred += Graph_UnrecoverableErrorOccurred;

				_graph.Start();
			}
			else
			{
				MessageDialog md = new MessageDialog("Cannot access microphone");
				await md.ShowAsync();
			}
		}

		private void Graph_UnrecoverableErrorOccurred(AudioGraph sender, AudioGraphUnrecoverableErrorOccurredEventArgs args)
		{
			throw new NotImplementedException();
		}

		private void AudioGraph_QuantumProcessed(AudioGraph sender, object args)
		{
			AudioFrame frame = _frameOutputNode.GetFrame();
			ProcessFrameOutput(frame);
		}

		unsafe private void ProcessFrameOutput(AudioFrame frame)
		{
			using (AudioBuffer buffer = frame.LockBuffer(AudioBufferAccessMode.ReadWrite))
			using (IMemoryBufferReference reference = buffer.CreateReference())
			{
				try
				{
					// Get the buffer from the AudioFrame
					((IMemoryBufferByteAccess)reference).GetBuffer(out byte* dataInBytes, out uint capacityInBytes);

					float* dataInFloat = (float*)dataInBytes;

					float max = 0;
					for (int i = 0; i < _graph.SamplesPerQuantum; i++)
					{
						max = Math.Max(Math.Abs(dataInFloat[i]), max);

					}

					currentPeak = max;



					//await TestAsync(max);

					//finalLevel = max;

					//if(max!=0)
					//Debug.WriteLine(max);

					//if (max != float.NaN)
					//{
					//	//int.TryParse(Math.Round(max * 20, 2).ToString(), out int level);

					//	Debug.WriteLine(Math.Round(max * 20, 0).ToString());



					//	foreach (Pixel pixel in Util.Context.Pixels)
					//	{
					//		if ((19 - pixel.Coord.Y) < Math.Round(max * 20, 0))
					//		{
					//			pixel.SetColor(new Color { B = (byte)(5 + pixel.Coord.Y * 5) });

					//			Debug.WriteLine(Math.Round(max * 20, 0).ToString()+ " - "+pixel.Coord.Y);

					//		}
					//		else
					//			pixel.SetColor(new Color());
					//	}




					//Util.SetLeds();
					//}

				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());

				}
			}
		}

		//private async System.Threading.Tasks.Task TestAsync(float max)
		//{
		//	await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {/*Your Code*/});
		//}

		// Using the COM interface IMemoryBufferByteAccess allows us to access the underlying byte array in an AudioFrame
		[ComImport]
		[Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		unsafe interface IMemoryBufferByteAccess
		{
			void GetBuffer(out byte* buffer, out uint capacity);
		}


		//[ComImport]
		//[Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
		//[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]

		//unsafe interface IMemoryBufferByteAccess
		//{
		//	void GetBuffer(out byte* buffer, out uint capacity);
		//}
	}
}
