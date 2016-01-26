namespace Synologen.Service.Client.Invoicing.Common {
	public static class Enumeration {

		public enum ConnectionStatus {
			Disconnected = 1,
			Connecting = 2,
			Connected = 3,
			Disconnecting = 4
		}

		public enum ViewMode {
			DisconnectedMode,
			ConnectedMode,
			StatusUpdateMode
		}
	}
}