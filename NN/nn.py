#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
Instruction:
In this section, you are asked to train a NN with different hyperparameters.
To start with training, you need to fill in the incomplete code. There are 3
places that you need to complete:
a) Backward pass equations for an affine layer (linear transformation + bias).
b) Backward pass equations for ReLU activation function.
c) Weight update equations with momentum.

After correctly fill in the code, modify the hyperparameters in "main()".
You can then run this file with the command: "python nn.py" in your terminal.
The program will automatically check your gradient implementation before start.
The program will print out the training progress, and it will display the
training curve by the end. You can optionally save the model by uncommenting
the lines in "main()".
"""

from __future__ import division
from __future__ import print_function

from util import LoadData, Load, Save, DisplayPlot
import os
import sys
import copy
import numpy as np
# import matplotlib
# matplotlib.use('TkAgg')
import matplotlib.pyplot as plt


# The following part is for sending data to Unity
import time
import zmq
import struct
context = zmq.Context()
socket = context.socket(zmq.REP)

# The following two worked
socket.bind("tcp://*:123")


def InitNN(num_inputs, num_hiddens, num_outputs):
    """
    Initializes NN parameters.

    Args:
        num_inputs:    Number of input units.
        num_hiddens:   List of two elements, hidden size for each layer.
        num_outputs:   Number of output units.

        num_inputs = 2304
        num_hiddens = [16, 32] (since we have two layers)
        num_outputs = 7

    Returns:
        model:         Randomly initialized network weights.
    """
    # randn creates an array of specified shape (ex, the first one is 2304x16)
    # and fills it with random values as per standard
    # normal distribution (mean 0 and variance 1); dwi, dbi are differential

    # W1b1 is the model that connect input to the first layer
    W1 = 0.1 * np.random.randn(num_inputs, num_hiddens[0])
    b1 = np.zeros((num_hiddens[0]))
    dW1 = np.zeros(W1.shape)
    db1 = np.zeros(b1.shape)

    # W2b2 is the model that connect first layer to the second layer
    W2 = 0.1 * np.random.randn(num_hiddens[0], num_hiddens[1])
    b2 = np.zeros((num_hiddens[1]))
    dW2 = np.zeros(W2.shape)
    db2 = np.zeros(b2.shape)


    # W3b3 is the model that connect the second layer to the output
    W3 = 0.01 * np.random.randn(num_hiddens[1], num_outputs)
    b3 = np.zeros((num_outputs))
    dW3 = np.zeros(W3.shape)
    db3 = np.zeros(b3.shape)

    # Dictionary of all the weights, biases and differentials
    model = {
        'W1': W1, 'W2': W2, 'W3': W3,
        'b1': b1, 'b2': b2, 'b3': b3,
        'dW1': dW1, 'dW2': dW2, 'dW3': dW3,
        'db1': db1, 'db2': db2, 'db3': db3

    }

    # Zhuoyue： print statement here
    # print(W1.shape, W2.shape, W3.shape, sep=" ")
    # (2304, 16) (16, 32) (32, 7)
    # print(b1.shape, b2.shape, b3.shape, sep=" ")
    # (16,) (32,) (7,)
    return model


def Affine(x, w, b):
    """
    Computes the affine transformation.
    *An affine function is the composition of a linear function with a translation,
   so while the linear part fixes the origin, the translation can map it somewhere else.
    Args:
        x: Inputs (or hidden layers)
        w: Weights
        b: Bias

    Returns:
        y: Outputs
    """
    # y = np.dot(w.T, x) + b
    y = x.dot(w) + b
    return y


def AffineBackward(grad_y, h, w):
    """
    Computes gradients of affine transformation.
    Here we don't have activation function

    Args:
    y is nx1, h is nxm, w is mx1, b is nx1
        grad_y: gradient from last layer
        h: inputs from the hidden layer
        w: weights

    Returns:
        grad_h: Gradients wrt. the inputs/hidden layer.
        grad_w: Gradients wrt. the weights.
        grad_b: Gradients wrt. the biases.
    """
    ###########################
    # so nx1 dot 1xm = nxm
    grad_h = grad_y.dot(w.T)
    grad_w = h.T.dot(grad_y)
    # axis = 0 along the column, 1 along the row.
    grad_b = np.sum(grad_y, axis=0)
    return grad_h, grad_w, grad_b
    ###########################


def ReLU(z):
    """Computes the ReLU activation function.

    Args:
        z: Inputs

    Returns:
        h: Activation of z
    """
    return np.maximum(z, 0.0)


def ReLUBackward(grad_y, z):
    """
    Computes gradients of the ReLU activation function
    wrt. the unactivated inputs.

    Returns:
        grad_z: Gradients wrt. the hidden state prior to activation.
    """
    ###########################
    # This is the definition of the ReLU activation function
    # numpy.maximum: element-wise maximum of array elements.
    y = np.maximum(0, z)
    # Since z bar = y bar * y', and if z > 0, y = z and dy/dz = dz/dz = 1
    y[y > 0] = 1
    # "*" will return an nx1 array(apply each term individually),
    # while dot() will return a number
    grad_z = y * grad_y
    return grad_z
    ###########################


def Softmax(x):
    """Computes the softmax activation function.

    Args:
        x: Inputs

    Returns:
        y: Activation
    """
    return np.exp(x) / np.exp(x).sum(axis=1, keepdims=True)


def NNForward(model, x):
    """Runs the forward pass.

    Args:
        model: Dictionary of all the weights.
        x:     Input to the network.

    Returns:
        var:   Dictionary of all intermediate variables.
    """
    z1 = Affine(x, model['W1'], model['b1'])
    h1 = ReLU(z1) # the shape is [100, 16] i.e. [batch_size, # neurons in hidden layer 1]
    z2 = Affine(h1, model['W2'], model['b2'])
    h2 = ReLU(z2) # the shape is [100, 32] i.e. [batch_size, # neurons in hidden layer 2]
    y = Affine(h2, model['W3'], model['b3']) # the shape is [100, 7]
    var = {
        'x': x, 'z1': z1, 'h1': h1,
        'z2': z2, 'h2': h2, 'y': y
    }
    return var


def NNBackward(model, err, var):
    """
    Runs the backward pass.
    dE_dh2 means the partial derivatives of E wrt. h2
    Args:
        model:    Dictionary of all the weights.
        err:      Gradients to the output of the network.
        var:      Intermediate variables from the forward pass.
    """
    dE_dh2, dE_dW3, dE_db3 = AffineBackward(err, var['h2'], model['W3'])
    dE_dz2 = ReLUBackward(dE_dh2, var['z2'])
    dE_dh1, dE_dW2, dE_db2 = AffineBackward(dE_dz2, var['h1'], model['W2'])
    dE_dz1 = ReLUBackward(dE_dh1, var['z1'])
    _, dE_dW1, dE_db1 = AffineBackward(dE_dz1, var['x'], model['W1'])
    model['dE_dW1'] = dE_dW1
    model['dE_dW2'] = dE_dW2
    model['dE_dW3'] = dE_dW3
    model['dE_db1'] = dE_db1
    model['dE_db2'] = dE_db2
    model['dE_db3'] = dE_db3
    pass


def NNUpdate(model, eps, momentum):
    """
    Update each of the weights in the network so that they cause the actual
    output to be closer the target output, thereby minimizing the error for
    each output neuron and the network as a whole.
    Args:
        model:    Dictionary of all the weights.
        eps:      Learning rate.
        momentum: Momentum.
    """
    ###########################
    # Update all the W and b
    for i in ["1", "2", "3"]:
        for j in ["W", "b"]:
            model['d' + j + i] = momentum * model['d' + j + i] - eps * model[
                'dE_d' + j + i]
            model[j + i] += model['d' + j + i]
    ###########################
def Evaluate(inputs, target, model, forward, batch_size=-1):
    """Evaluates the model on inputs and target.

    inputs's shape is 419 * 2304, means 419 pictures of 48*48
    batch_size is 100 by default

    Args:
        inputs: Inputs to the network.
        target: Target of the inputs.
        model:  Dictionary of network weights.
    """
    num_cases = inputs.shape[0]
    if batch_size == -1:
        batch_size = num_cases
    num_steps = int(np.ceil(num_cases / batch_size))
    ce = 0.0
    acc = 0.0
    for step in range(num_steps):
        start = step * batch_size
        end = min(num_cases, (step + 1) * batch_size)
        x = inputs[start: end]
        t = target[start: end]
        prediction = Softmax(forward(model, x)['y'])
        ce += -np.sum(t * np.log(prediction))
        acc += (np.argmax(prediction, axis=1) == np.argmax(
            t, axis=1)).astype('float').sum()
    ce /= num_cases
    acc /= num_cases
    return ce, acc


def CheckGrad(model, forward, backward, name, x):
    """
    Check the gradients

    Args:
        model: Dictionary of network weights.
        name: Weights name to check.
        x: Fake input.
    """
    np.random.seed(0)
    var = forward(model, x)
    loss = lambda y: 0.5 * (y ** 2).sum() # this seems like a list square loss
    grad_y = var['y']
    backward(model, grad_y, var)
    grad_w = model['dE_d' + name].ravel()
    w_ = model[name].ravel()
    eps = 1e-7
    grad_w_2 = np.zeros(w_.shape)
    check_elem = np.arange(w_.size)
    np.random.shuffle(check_elem)
    # Randomly check 20 elements.
    check_elem = check_elem[:20]
    for ii in check_elem:
        w_[ii] += eps
        err_plus = loss(forward(model, x)['y'])
        w_[ii] -= 2 * eps
        err_minus = loss(forward(model, x)['y'])
        w_[ii] += eps
        grad_w_2[ii] = (err_plus - err_minus) / 2 / eps
    np.testing.assert_almost_equal(grad_w[check_elem], grad_w_2[check_elem],
                                   decimal=3)

def Train(model, forward, backward, update, eps, momentum, num_epochs,
          batch_size):
    """Trains a simple MLP.

    Args:
        model:           Dictionary of model weights.
        forward:         Forward prop function.
        backward:        Backward prop function.
        update:          Update weights function.
        eps:             Learning rate.
        momentum:        Momentum.
        num_epochs:      Number of epochs to run training for.
        * One epoch means that each sample in the training dataset has had an opportunity to
        update the internal model parameters. An epoch is comprised of one or more batches
        batch_size:      Mini-batch size, -1 for full batch.

    Returns:
        stats:           Dictionary of training statistics.
            - train_ce:       Training cross entropy.
            - valid_ce:       Validation cross entropy.
            - train_acc:      Training accuracy.
            - valid_acc:      Validation accuracy.
    """
    inputs_train, inputs_valid, inputs_test, target_train, target_valid, \
    target_test = LoadData('./toronto_face.npz')
    # inputs_train's shape is 3374 * 2304
    # inputs_train's shape is 419 * 2304
    rnd_idx = np.arange(inputs_train.shape[0])
    train_ce_list = []
    valid_ce_list = []
    train_acc_list = []
    valid_acc_list = []
    num_train_cases = inputs_train.shape[0]
    if batch_size == -1:
        batch_size = num_train_cases
    num_steps = int(np.ceil(num_train_cases / batch_size))
    for epoch in range(num_epochs):
        np.random.shuffle(rnd_idx)
        inputs_train = inputs_train[rnd_idx]
        target_train = target_train[rnd_idx]
        for step in range(num_steps):
            # Forward prop.
            start = step * batch_size
            end = min(num_train_cases, (step + 1) * batch_size)
            x = inputs_train[start: end]
            t = target_train[start: end]

            var = forward(model, x)
            prediction = Softmax(var['y'])

            train_ce = -np.sum(t * np.log(prediction)) / x.shape[0] # 这里用的是cross entropy loss, 但问题是，实际做back prop的是下面的error，那个只是简单的 错误百分数(但其实那个简单的百分数就是 grad_y...我佛了)
            train_acc = (np.argmax(prediction, axis=1) ==
                         np.argmax(t, axis=1)).astype('float').mean()
            # print(('Epoch {:3d} Step {:2d} Train CE {:.5f} '
            #        'Train Acc {:.5f}').format(
            #     epoch, step, train_ce, train_acc))

            # # ### If we want to send the sonification of every steps (the `1` at last indicates this is a train data)
            # dataToUnity = ('{:.5f},''{:.5f},0').format(train_ce, train_acc)
            # # #  Wait for next request from client
            # message = socket.recv()
            # # #  Send reply back to client
            # socket.send(str.encode(dataToUnity)) # send data to unity
            # time.sleep(0.1)

            # Compute error.
            error = (prediction - t) / x.shape[0] # this is actually the grad_y, i.e. d_loss / d_y
            # Backward prop.
            backward(model, error, var)
            # Update weights.
            update(model, eps, momentum)

        valid_ce, valid_acc = Evaluate(
            inputs_valid, target_valid, model, forward, batch_size=batch_size)

        data_stream = ('Epoch {:3d} '
               'Validation CE {:.5f} '
               'Validation Acc {:.5f}\n').format(
            epoch, valid_ce, valid_acc)
        print(data_stream)


        new_model = copy.deepcopy(model)  # make a copy of the model, so we don't mess it up
        momentum_changed_amount = 0
        eps_changed_amount = 0
        while 1:
            print()
            print("num_hiddens: " + str(num_hiddens))
            print("epsilon: " + str(eps))
            print("momentum: " + str(momentum))
            # Calculate weights per link
            W1ByLinks = np.mean(model['W1'], axis = 0) # model['W1'] is 2304 * 16, so we need take mean along 0 axis and become 1 * 16
            W2ByLinks = model['W2'].flatten()  # fatten the 2D array into 1D
            W3ByLinks = np.mean(model['W3'], axis = 1)

            # print(model['W1'].shape) # (input * layer 1) -> (2304, 3)
            # print(model['W2'].shape) # (layer1 * layer 2) -> (3,4)
            # print(model['W3'].shape) # (layer2 * 7) -> (4,7)

            # Since some of the values are negative, we need to normalize them, so the force calculation is not in wrong direction
            # ptp is the range of values (maximum - minimum) along an axis. The name of the function comes from the acronym for ‘peak to peak’.
            W1ByLinksNormalized = (W1ByLinks - np.min(W1ByLinks))/np.ptp(W1ByLinks)
            W2ByLinksNormalized = (W2ByLinks - np.min(W2ByLinks))/np.ptp(W2ByLinks)
            W3ByLinksNormalized = (W3ByLinks - np.min(W3ByLinks))/np.ptp(W3ByLinks)

            # Since we want to send these weight matrix through TCP, we need to turn those value into string
            W1ByLinksString = '_'.join(str(w1) for w1 in W1ByLinksNormalized)
            # W2ByLinksString = '_'.join('_'.join(str(x) for x in y) for y in W2ByLinksNormalized) #if W2 is not flattened, we should use this
            W2ByLinksString = '_'.join(str(w2) for w2 in W2ByLinksNormalized)
            W3ByLinksString = '_'.join(str(w3) for w3 in W3ByLinksNormalized)

            # #  Wait for next request from client
            # print(dataToUnity)
            while 1:
                msg = socket.recv()
                message = msg.decode('ascii')
                print("Message from Unity: " + message)
                if message != "ChucK": # wait until we get the Oculus client
                    break
                dataToUnity = ('99,0')
                socket.send(str.encode(dataToUnity)) # should send message back if it's not the thing we want
            if message == "nothing":
                # ## If we send the sonification of every epoch (the `1` at last indicates this is a validation data)
                dataToUnity = ('0,{:.5f},{:.5f},1,{},{},{}').format(valid_ce, valid_acc, W1ByLinksString, W2ByLinksString, W3ByLinksString)
                socket.send(str.encode(dataToUnity))  # send data to unity
                while 1:
                    msg = socket.recv()
                    message = msg.decode('ascii')
                    if message == "ChucK":  # wait until we get the ChucK client
                        dataToUnity = ('0,{:.5f},{:.5f},1').format(valid_ce, valid_acc)
                        socket.send(str.encode(dataToUnity))  # send data to unity
                        break
                    dataToUnity = ('99,0')
                    socket.send(str.encode(dataToUnity))
                break
            if message != "wait":
                splittedMsg = message.split("_")
                mode = splittedMsg[-1]
                if mode == "updateMomentum":
                    momentum_changed_amount = 0.1 * float(splittedMsg[0]) # 这里其实，我们作业做的时候，eps都是0.001, 0.01, 0.1 0.5这种的，momentum是0, 0.5, 0.9这种的，所以我们可能需要调整一下值。目前是简单地*0.1处理了，但应该有更好的办法。
                    print("update momentum amount: " + str(momentum_changed_amount))
                    while 1:
                        dataToUnity = ('99,0')
                        socket.send(str.encode(dataToUnity))
                        msg = socket.recv()
                        message = msg.decode('ascii')
                        if message == "ChucK":  # wait until we get the ChucK client
                            dataToUnity = ('1,{:.5f}').format(momentum_changed_amount)
                            socket.send(str.encode(dataToUnity))  # send data to unity
                            break

                elif mode == "updateEps":
                    eps_changed_amount = 0.1 * float(splittedMsg[0])
                    print("update eps amount: " + str(eps_changed_amount))
                    while 1:
                        dataToUnity = ('99,0')
                        socket.send(str.encode(dataToUnity))
                        msg = socket.recv()
                        message = msg.decode('ascii')
                        if message == "ChucK":  # wait until we get the ChucK client
                            dataToUnity = ('2,{:.5f}').format(eps_changed_amount)
                            socket.send(str.encode(dataToUnity))  # send data to unity
                            break

                elif mode == "updateWeights":
                    nodeTag = int(splittedMsg[-2])
                    nodeID = int(splittedMsg[-3])
                    nodeVals = [float(val) for val in splittedMsg[0:-3]]
                    print("L2 weights:")
                    print(nodeVals)
                    if nodeTag == 2: # L2 nodes
                        valOut = nodeVals[-1]
                        valsL1 = nodeVals[0:-1]
                        new_model['W2'][:,nodeID] = valsL1 * model['W2'][:,nodeID]
                        new_model['W3'][nodeID,:] = valOut * model['W3'][nodeID,:]
                    elif nodeTag == 0: # Input node
                        new_model['W1'] = nodeVals * model['W1']
                    print("New model's W3:")
                    print(new_model['W3'])
                    new_valid_ce, new_valid_acc = Evaluate(inputs_valid, target_valid, new_model, forward, batch_size=batch_size)
                    data_stream = ('Updating Epoch {:3d} ''Validation CE {:.5f} ''Validation Acc {:.5f}\n').format(epoch, new_valid_ce, new_valid_acc)
                    print(data_stream)
                    while 1:
                        dataToUnity = ('99,0')
                        socket.send(str.encode(dataToUnity))
                        msg = socket.recv()
                        message = msg.decode('ascii')
                        if message == "ChucK":  # wait until we get the ChucK client
                            dataToUnity = ('3,{:.5f},{:.5f},1').format(new_valid_ce, new_valid_acc)
                            socket.send(str.encode(dataToUnity))  # send data to unity
                            break
                elif mode == "updateNodes":
                    socket.send(str.encode("received")) # send data to unity
                    layer1Count = splittedMsg[0]
                    layer2Count = splittedMsg[1]
                    new_argv = [sys.argv[0], layer1Count, layer2Count]
                    os.execv(sys.executable, ['python'] + new_argv) # re-run the script with new number of nodes
                    # then restart the program
                else: # if nothing, but just waiting, call it mode 99
                    dataToUnity = ('99,0')
                    socket.send(str.encode(dataToUnity)) # send data to unity
            else: # if nothing, but just waiting, call it mode 99
                dataToUnity = ('99,0')
                socket.send(str.encode(dataToUnity)) # send data to unity

            # if we received the "wait" message, put the process into sleep
            # 下面这个0.5会导致Unity那边换node个数时卡顿一下，不大好。
            # time.sleep(0.5) # 这里或许会有点问题，如果信号是一个queue的话，极有可能我们只是缓慢地遍历这个queue而已，而非我们想要的 0.5s看一下这个queue有啥
        model = new_model
        momentum = momentum + momentum_changed_amount
        eps = eps + eps_changed_amount
        # #  Send reply back to client
        train_ce_list.append((epoch, train_ce)) # 哦，这里append进去的应该是train的最后一个step的ce...
        train_acc_list.append((epoch, train_acc))
        valid_ce_list.append((epoch, valid_ce))
        valid_acc_list.append((epoch, valid_acc))
        # 下面这两个不显示，整个program跑起来会快很多
        # DisplayPlot(train_ce_list, valid_ce_list, 'Cross Entropy', number=0)
        # DisplayPlot(train_acc_list, valid_acc_list, 'Accuracy', number=1)

    print()
    train_ce, train_acc = Evaluate(
        inputs_train, target_train, model, forward, batch_size=batch_size)
    valid_ce, valid_acc = Evaluate(
        inputs_valid, target_valid, model, forward, batch_size=batch_size)
    test_ce, test_acc = Evaluate(
        inputs_test, target_test, model, forward, batch_size=batch_size)
    print('CE: Train %.5f Validation %.5f Test %.5f' %
          (train_ce, valid_ce, test_ce))
    print('Acc: Train {:.5f} Validation {:.5f} Test {:.5f}'.format(
        train_acc, valid_acc, test_acc))

    stats = {
        'train_ce': train_ce_list,
        'valid_ce': valid_ce_list,
        'train_acc': train_acc_list,
        'valid_acc': valid_acc_list
    }

    return model, stats



# Default hyper-parameters. (These variables should be global)
num_hiddens = [3, 4]
eps = 0.01
momentum = 0.0
num_epochs = 10000
batch_size = 100

def main():
    """Trains a NN."""
    # NPZ is a file format by numpy that provides storage
    # of array data using gzip compression.
    model_fname = 'nn_model.npz'
    stats_fname = 'nn_stats.npz'

    # Input-output dimensions.
    num_inputs = 2304
    num_outputs = 7

    # Initialize model.
    model = InitNN(num_inputs, num_hiddens, num_outputs)

    # Uncomment to reload trained model here.
    # model = Load(model_fname)

    # Check gradient implementation.
    print('Checking gradients...')
    x = np.random.rand(10, 48 * 48) * 0.1
    CheckGrad(model, NNForward, NNBackward, 'W3', x)
    CheckGrad(model, NNForward, NNBackward, 'b3', x)
    CheckGrad(model, NNForward, NNBackward, 'W2', x)
    CheckGrad(model, NNForward, NNBackward, 'b2', x)
    CheckGrad(model, NNForward, NNBackward, 'W1', x)
    CheckGrad(model, NNForward, NNBackward, 'b1', x)

    # Train model.
    stats = Train(model, NNForward, NNBackward, NNUpdate, eps,
                  momentum, num_epochs, batch_size)

    # Uncomment if you wish to save the model.
    # Save(model_fname, model)

    # Uncomment if you wish to save the training statistics.
    # Save(stats_fname, stats)


if __name__ == '__main__':
    # print(sys.argv)
    if len(sys.argv) > 1: # sys.argv looks like ['nn.py', 'arg1', 'arg2', ....]
        num_hiddens[0] = int(sys.argv[1])
        num_hiddens[1] = int(sys.argv[2])
    main()
