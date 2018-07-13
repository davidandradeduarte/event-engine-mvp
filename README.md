# Introduction

# Setting up the queue



# Rabbit MQ 

Dump messages from a rabbitmq queue without removing them from the queue:

    $ mkdir dump
    $ rabbitmq-dump-queue -uri="amqp://guest:guest@localhost:5672/" -queue=test_queue -max-messages=1 -output-dir=./dump
    $ cat ./dump/msg-000

`rabbitmq-dump-queue` is a go app from here: https://github.com/dubek/rabbitmq-dump-queue

