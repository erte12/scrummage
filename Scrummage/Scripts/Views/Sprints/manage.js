var source = $('#taskRow').html();
var template = Handlebars.compile(source);
var table = $('#tasks');

function bindSprintUpdateEvent(sprintId) {
    $('#sprintForm').validate({
        rules: {
            name: {
                required: true,
                minlength: 3,
                maxlength: 60
            },
            description: {
                maxlength: 1000
            }
        },
        submitHandler: function () {
            $.ajax({
                    url: '/Api/Sprints/' + sprintId,
                    method: 'patch',
                    data: $('#sprintForm').serialize()
                })
                .done(function () {
                    toastr.success('Changes has been saved');
                })
                .fail(function (data) {
                    console.log(data.responseJSON.modelState);
                    toastr.error('Something went wrong');
                });

            return false;
        }
    });

    $("#sprintForm").enter();
}

function bindSprintDeleteEvent() {
    $('#deleteButton').on('click', function (event) {
        event.preventDefault();
        bootbox.confirm({
            title: "Delete sprint?",
            message: "Do you want to delete this sprint? This cannot be undone.",
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                if (result === true)
                    $('#deleteButton').parents('form').submit();
            }
        });
    });
}

function loadScrumTasksTableData(sprintId) {
    $.get('/Api/ScrumTasks?sprintId=' + sprintId, function (data) {
        data.forEach(function (task) {
            var taskRowHtml = template(task);
            $('#tasks tbody').append(taskRowHtml);
        });
    }).done(function () {
        table = table.DataTable({
            "order": [6, 'desc']
        });
    });
}

function bindScrumTasksCreateEvent() {
    $('#newTask').validate({
        rules: {
            title: {
                required: true,
                minlength: 3,
                maxlength: 40
            },
            content: {
                required: true,
                minlength: 3,
                maxlength: 400
            }
        },
        submitHandler: function () {

            $.ajax({
                    url: '/Api/ScrumTasks/',
                    method: 'post',
                    data: $('#newTask').serialize()
                })
                .done(function (data) {
                    toastr.success('New task has been created');
                    var taskRowHtml = template(data);
                    $('#newTask').find("input[type=text], textarea").val("");
                    $('#newTask').find("input[type=text]").focus();
                    table.row.add($(taskRowHtml)).draw();
                })
                .fail(function (data) {
                    console.log(data);
                    toastr.error('Something went wrong');
                });

            return false;
        }
    });

    $("#newTask").enter();
}

function bindScrumTasksUserUpdateEvent() {
    $('#tasks tbody').on('change', '.js-members', function () {
        var userId = $(this).val();
        var scrumTaskId = $(this).parents('tr').attr('data-task-id');

        $.ajax({
            url: '/api/scrumtasks/' + scrumTaskId,
            method: 'patch',
            data: { userId: userId }
        })
        .done(function () {
            toastr.success('User set successfully');
        })
        .fail(function () {
            toastr.error('Something went wrong');
        });
    });
}

function bindScrumTasksEstimationUpdateEvent() {
    $('#tasks tbody').on('change', '.js-estimations', function () {
        var estimationId = $(this).val();
        var scrumTaskId = $(this).parents('tr').attr('data-task-id');

        $.ajax({
                url: '/api/scrumtasks/' + scrumTaskId,
                method: 'patch',
                data: { estimationId: estimationId }
            })
            .done(function () {
                toastr.success('Estimation set successfully');
            })
            .fail(function () {
                toastr.error('Something went wrong');
            });
    });
}

function bindScrumTasksPriorityUpdateEvent() {
    $('#tasks tbody').on('change', '.js-priority', function () {
        var priority = $(this).val();
        var scrumTaskId = $(this).parents('tr').attr('data-task-id');

        $.ajax({
            url: '/api/scrumtasks/' + scrumTaskId,
            method: 'patch',
            data: { priority: priority }
        })
        .done(function () {
            toastr.success('Priority changed successfully');
        })
        .fail(function () {
            toastr.error('Something went wrong');
        });
    });
}

function bindScrumTasksDeleteEvent() {
    $('#tasks tbody').on('click', '.js-delete', function () {
        var taskRow = $(this).parents('tr');
        var taskId = taskRow.attr('data-task-id');

        bootbox.confirm({
            title: "Delete task?",
            message: "Do you want to delete this task? This cannot be undone.",
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                if (result === true) {
                    $.ajax({
                        url: '/api/scrumtasks/' + taskId,
                        method: 'delete'
                    })
                    .done(function () {
                        toastr.success('Task successfully deleted');
                        table.rows(taskRow).remove().draw();
                    })
                    .fail(function () {
                        toastr.error('Something went wrong');
                    });
                }
            }
        });
    });
}

function bindScrumTasksContentUpdateEvent() {
    $('#tasks tbody').on('click', '.js-editable', function () {
        var contentElement = $(this);
        var content = contentElement.text();
        var scrumTaskId = contentElement.parents('tr').attr('data-task-id');
        var dataType = contentElement.parents('td').attr('data-type');

        bootbox.prompt({
            title: "Edit task's " + dataType,
            inputType: 'textarea',
            value: content,
            callback: function (newValue) {
                if (newValue != null) {
                    $.ajax({
                        url: '/api/scrumtasks/' + scrumTaskId,
                        method: 'patch',
                        data: (dataType === 'content') ? { content: newValue } : { title: newValue } //TODO: change needed...
                    })
                    .done(function () {
                        toastr.success(dataType[0].toUpperCase() + dataType.substring(1) + ' updated successfully');
                        contentElement.html(newValue);
                    })
                    .fail(function () {
                        toastr.error('Something went wrong');
                    });
                }
            }
        });
    });
}