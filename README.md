# Case00041696

## Services / Applications

Each of the following will need to be run at startup to see the whole picture.

### UserApplication.App

This is your start point. This represents an occasionally connected client, like a WPF or web browser. It does not run NServiceBus, insteads it talks to a website back end running WebAPI and SignalR.

It submits the batch via a POST request to UserApplication.WebBackend.
It listens for events via SignalR and writes them to the console.

### UserApplication.WebBackend

This is a WebAPI and SignalR server.

The WebAPI provides a way to submit batches and converts the request to a message through NServiceBus which is sent to the endpoint that can process the Batch.

The SignalR server subscribes to batch events and emits them to connected clients, translating them into status messages.

### BatchProcessingEndpoint

Orchestrates batch level items using a Saga to track and report the entire batch's progress.

### BatchDataItemProcessingEndpoint

Orchestrates the processing batch data level items using a Saga to manage a workflow for each data item.

### LongRunningProcessEndpoint

This performs message processing that is longer than usual.

### OperationsEndpoint

This performs miscellaneous operations for the system.

### ParticularPlatform

This will launch ServiceControl, Monitoring, and ServicePulse. You can use ServicePulse to retry failed messages and view endpoint metrics.

### UnreliableService

This is a web service that is up intermittently.

### UnreliableServiceEndpoint

This is an NServiceBus endpoint that makes calls to the UnreliableService.

### ValidationEndpoint

This endpoint performs validation on batch data items.

## Contracts / Configuration

### Messages

This contains all the NServiceBus message contracts

### UserApplication.Contracts

This contains a contract between the UserApplication.App and the UserApplication.WebBackend WebAPI Batch controller.

## Configuration

ParticularPlatform will find free ports automatically.

ServerInfo.cs defines the url/ports used by the 2 web applications.

Check startup projects to make sure they are all able to run.

Everything else should be fire and forget.
